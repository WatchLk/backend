using WatchLk.ProductProcessing.Domains.Dto;
using WatchLk.ProductProcessing.Domains.Models;

namespace WatchLk.ProductProcessing.Domains.Mappings
{
    public static class ProductMapper
    {
        public static Product ToEntity(this CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageURLs = dto.ImageURLs,
                BrandId = dto.BrandId,
                CategoryId = dto.CategoryId,
            };
        }

        public static Product ToEntity(this UpdateProductDto dto, int id)
        {
            return new Product
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageURLs = dto.ImageURLs,
                BrandId = dto.BrandId,
                CategoryId = dto.CategoryId,
            };
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.ImageURLs,
                product.BrandId,
                product.Brand!.ToDto(),
                product.CategoryId,
                product.Category!.ToDto(),
                product.CreatedAt,
                product.UpdatedAt
            );
        }
    }
}
