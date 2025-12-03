using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Configurations;
using WebApp.Services;

namespace WebApp.Extensions;

public static class DatabaseSeederExtensions
{
    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var embeddingsService = scope.ServiceProvider.GetRequiredService<EmbeddingsService>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
        
        try
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            
            // Check if data already exists
            if (await context.Books.AnyAsync())
            {
                logger.LogInformation("Database already seeded. Skipping seed operation.");
                return;
            }
            
            logger.LogInformation("Starting database seeding...");
            
            // Get seed data from configuration
            var seedData = BookConfiguration.GetSeedData();
            
            // Generate embeddings for each book
            foreach (var book in seedData)
            {
                logger.LogInformation("Generating embedding for: {BookName}", book.Name);
                
                // Combine name and description for embedding
                var textToEmbed = $"{book.Name}. {book.Description}";
                
                // Generate embedding and convert to SqlVector
                var embeddingArray = await embeddingsService.EmbedAsync(textToEmbed);
                book.Embedding = new SqlVector<float>(embeddingArray);
                
                logger.LogInformation("Embedding generated successfully for: {BookName}", book.Name);
            }
            
            // Add all books to database
            await context.Books.AddRangeAsync(seedData);
            await context.SaveChangesAsync();
            
            logger.LogInformation("Database seeding completed successfully. {Count} books seeded.", seedData.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}

