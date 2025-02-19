using Microsoft.EntityFrameworkCore;
using WatchLk.ProductProcessing.Domains.Dto;
using WatchLk.ProductProcessing.Domains.Mappings;
using WatchLk.ProductProcessing.Infrastructure;

namespace WatchLk.ProductProcessing.Application
{
    public class ProductRepository(ProductDbContext dbContext) : IProductRepository
    {

        private readonly ProductDbContext _dbContext = dbContext;

        public async Task<ProductDto?> CreateProductAsync(CreateProductDto productDto)
        {
            try
            {
                var newProduct = productDto.ToEntity();

                await _dbContext.Products.AddAsync(newProduct);
                await _dbContext.SaveChangesAsync();

                newProduct = await _dbContext.Products
                                            .Include(product => product.Brand)
                                            .Include(product => product.Category)
                                            .FirstOrDefaultAsync(product => product.Id == newProduct.Id);

                return newProduct?.ToDto();
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(int? brandId, int? categoryId, string? search, string? sortBy, string? orderBy, int page = 1, int pageSize = 10)
        {
            try
            {

                var query = _dbContext.Products
                                    .AsNoTracking();

                if (brandId.HasValue)
                {
                    query = _dbContext.Products.Where(product => product.BrandId == brandId);
                }

                if (categoryId.HasValue)
                {
                    query = _dbContext.Products.Where(product => product.CategoryId == categoryId);
                }

                if (!string.IsNullOrEmpty(search)) 
                {
                    query = _dbContext.Products.Where(product => product.Name.Contains(search) || product.Description.Contains(search));
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    query = sortBy.ToLower() switch
                    {
                        "price" => orderBy?.ToLower() == "desc" ? query.OrderByDescending(product => product.Price) : query.OrderBy(product => product.Price),
                        "name" => orderBy?.ToLower() == "desc" ? query.OrderByDescending(product => product.Name) : query.OrderBy(product => product.Name),
                        _ => orderBy?.ToLower() == "desc" ? query.OrderByDescending(product => product.Price) : query.OrderBy(product => product.Price)
                    };
                }

                var skip = (page - 1) * pageSize;

                var products = await query
                    .Include(product => product.Brand)
                                    .Include(product => product.Category)
                    .Select(product => product.ToDto())
                                          .Skip(skip)
                                          .Take(pageSize)
                                          .ToListAsync();

                return products;

            } catch (Exception) 
            {
                throw;
            }
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products
                                    .Include(product => product.Brand)
                                    .Include(product => product.Category)
                                    .FirstOrDefaultAsync(product => product.Id == id);

                if (product is null)
                {
                    return null;
                }

                return product.ToDto();
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            try
            {
                var existingProduct = await _dbContext.Products.FindAsync(id);

                if (existingProduct is null)
                {
                    return null;
                }

                existingProduct.Name = productDto.Name;
                existingProduct.Description = productDto.Description;
                existingProduct.Price = productDto.Price;
                existingProduct.BrandId = productDto.BrandId;
                existingProduct.CategoryId = productDto.CategoryId;

                await dbContext.SaveChangesAsync();

                var updatedProduct = await _dbContext.Products
                                                     .Include(product => product.Brand)
                                                     .Include(product => product.Category)
                                                     .FirstOrDefaultAsync(product => product.Id == id);

                return updatedProduct?.ToDto();
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                await _dbContext.Products.Where(product => product.Id == id)
                                  .ExecuteDeleteAsync();

                return ;
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}
