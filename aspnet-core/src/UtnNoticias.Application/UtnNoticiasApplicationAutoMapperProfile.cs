using AutoMapper;
using UtnNoticias.News;
using UtnNoticias.Themes;
using UtnNoticias.User;
using Volo.Abp.Identity;

namespace UtnNoticias;

public class UtnNoticiasApplicationAutoMapperProfile : Profile
{
	public UtnNoticiasApplicationAutoMapperProfile()
	{
		/* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
		CreateMap<Theme, ThemeDto>();
		CreateMap<IdentityUser, UserDto>();
		CreateMap<NewsDto, ArticleDto>().ReverseMap();

	}
}
