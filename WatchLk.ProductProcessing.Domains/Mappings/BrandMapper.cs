using WatchLk.ProductProcessing.Domains.Dto;
using WatchLk.ProductProcessing.Domains.Models;

namespace WatchLk.ProductProcessing.Domains.Mappings
{
    public static class BrandMapper
    {
        public static Brand ToEntity(this BrandDto dto)
        {
            return new Brand
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        public static BrandDto ToDto(this Brand brand)
        {
            return new BrandDto(brand.Id, brand.Name);
        }
    }
}
