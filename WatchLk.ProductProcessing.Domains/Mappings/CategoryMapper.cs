using WatchLk.ProductProcessing.Domains.Dto;
using WatchLk.ProductProcessing.Domains.Models;

namespace WatchLk.ProductProcessing.Domains.Mappings
{
    public static class CategoryMapper
    {
        public static Category ToEntity(this CategoryDto dto)
        {
            return new Category
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto(category.Id, category.Name);
        }
    }
}
