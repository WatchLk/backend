using WatchLk.ProductProcessing.Domains.Dto;

namespace WatchLk.ProductProcessing.Application
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProductsAsync(
             int? brandId,
             int? categoryId,
             string? search,
             string? sortBy,
             string? orderBy,
             int page = 1,
             int pageSize = 10);

        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto?> CreateProductAsync(CreateProductDto productDto);
        Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto productDto);
        Task DeleteProductAsync(int id);
    }
}
