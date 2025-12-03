using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class SearchController(
    AppDbContext context,
    EmbeddingsService embeddingsService,
    ILogger<SearchController> logger)
    : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(string searchQuery, string? threshold)
    {
        try
        {
            List<BookSearchResultDto> books;
            double? thresholdValue = null;
            
            // Parse threshold if provided (use InvariantCulture to ensure "0.6" works correctly)
            if (!string.IsNullOrWhiteSpace(threshold) && 
                double.TryParse(threshold, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedThreshold))
            {
                thresholdValue = parsedThreshold;
            }

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                logger.LogInformation("Returning all books (no search query provided)");

                // Return all books when no search query
                books = await context.Books
                    .Select(b => new BookSearchResultDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Description = b.Description.Length > 200 
                            ? b.Description.Substring(0, 200) + "..." 
                            : b.Description,
                        FullDescription = b.Description,
                        Distance = null
                    })
                    .ToListAsync();
            }
            else
            {
                logger.LogInformation("Searching for: {SearchQuery} with threshold: {Threshold}", searchQuery, thresholdValue);

                // Generate embedding for search query
                var embeddingArray = await embeddingsService.EmbedAsync(searchQuery);
                var searchEmbedding = new SqlVector<float>(embeddingArray);

                // Perform vector similarity search
                var query = context.Books
                    .Select(b => new BookSearchResultDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Description = b.Description.Length > 200 
                            ? b.Description.Substring(0, 200) + "..." 
                            : b.Description,
                        FullDescription = b.Description,
                        Distance = EF.Functions.VectorDistance("cosine", b.Embedding, searchEmbedding)
                    });

                // Apply threshold filter if provided
                if (thresholdValue.HasValue)
                {
                    query = query.Where(b => b.Distance <= thresholdValue.Value);
                }

                books = await query.OrderBy(b => b.Distance).ToListAsync();
            }

            logger.LogInformation("Found {Count} books", books.Count);

            return Json(new { success = true, books });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error performing search");
            return Json(new { success = false, message = "An error occurred while searching. Please try again." });
        }
    }
}

