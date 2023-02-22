using AutoMapper;
using ProductManagement.Domain.Common;

namespace ProductManagement.Application.AutoMapperProfiles
{
    public class PagedProfile : Profile
    {
        public PagedProfile()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResponse<>));
        }
    }
}