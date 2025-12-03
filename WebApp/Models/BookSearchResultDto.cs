namespace WebApp.Models;

public class BookSearchResultDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string FullDescription { get; set; } = default!;
    public double? Distance { get; set; }
}

