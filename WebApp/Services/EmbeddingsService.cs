using System.Text.Json;
using Azure;
using Azure.AI.Inference;

namespace WebApp.Services;

public class EmbeddingsService(IConfiguration configuration)
{
    private EmbeddingsClient GetClient() => new( new Uri(configuration["MicrosoftFoundry:Url"]!), new AzureKeyCredential(configuration["MicrosoftFoundry:key"]!));

    public async Task<float[]> EmbedAsync(string text, CancellationToken ct = default)
    {
        return (await EmbedAsync([text], ct)).First();
    }
    
    public async Task<List<float[]>> EmbedAsync(IEnumerable<string> texts, CancellationToken ct = default)
    {
        var options = new EmbeddingsOptions(texts)
        {
            Model = configuration["MicrosoftFoundry:Model"]
        };
        
        var client = GetClient();
        Response<EmbeddingsResult> response = await client.EmbedAsync(options, ct);

        return response.Value.Data.Select(d => 
            ConvertToFloatArray(d.Embedding)).ToList();
    }

    private static float[] ConvertToFloatArray(BinaryData embedding)
    {
        return JsonSerializer.Deserialize<float[]>(embedding.ToArray())!;
    }
}