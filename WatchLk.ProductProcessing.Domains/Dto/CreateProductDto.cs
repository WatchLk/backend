using System.ComponentModel.DataAnnotations;

namespace WatchLk.ProductProcessing.Domains.Dto
{
    public record CreateProductDto
    (
        [Required]
        string Name,
        [Required]
        string Description,
        [Required]
        [Range(1,Double.MaxValue)]
        double Price,
        [Required]
        [Range(0, 1000)]
        int Stock,
        IList<string> ImageURLs,
        [Required]
        int BrandId,
        [Required]
        int CategoryId
    );
}
