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
            
            logger.LogInformation("Starting database seeding...");
            
            // Get seed data from configuration
            var seedData = BookConfiguration.GetSeedData();
            
            // Generate embeddings for each book
            foreach (var book in seedData)
            {
                var existingByName = await context.Books
                    .FirstOrDefaultAsync(b => b.Name == book.Name);
                
                if (existingByName != null)
                {
                    logger.LogInformation("Book with name '{BookName}' already exists (ID: {ExistingId}). Skipping.", 
                        book.Name, existingByName.Id);
                    continue;
                }
                
                logger.LogInformation("Generating embedding for: {BookName} (ID: {BookId})", book.Name, book.Id);
                
                // Combine name and description for embedding
                var textToEmbed = $"{book.Name}. {book.Description}";
                
                // Generate embedding and convert to SqlVector
                var embeddingArray = await embeddingsService.EmbedAsync(textToEmbed);
                book.Embedding = new SqlVector<float>(embeddingArray);
                
                logger.LogInformation("Embedding generated successfully for: {BookName}", book.Name);
                
                // Add book to database
                await context.Books.AddAsync(book);
            }
            
            // Save all changes
            var savedCount = await context.SaveChangesAsync();
            
            logger.LogInformation("Database seeding completed successfully. {Count} books seeded.", savedCount);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}

