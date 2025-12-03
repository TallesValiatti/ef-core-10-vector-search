using Microsoft.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class Book
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public SqlVector<float> Embedding { get; set; } = default!;
}

