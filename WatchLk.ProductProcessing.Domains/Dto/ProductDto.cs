namespace WatchLk.ProductProcessing.Domains.Dto

{
    public record ProductDto
    (
        int Id,
        string Name,
        string Description,
        double Price,
        int Stock,
        IList<string> ImageURLs,
        int BrandId,
        BrandDto? Brand,
        int CategoryId,
        CategoryDto? Category,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
