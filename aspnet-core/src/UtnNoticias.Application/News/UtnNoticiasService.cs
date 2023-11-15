using UtnNoticias.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace UtnNoticias.News
{
	public class UtnNoticiasService : UtnNoticiasAppService, IUtnNoticiasService
	{
		private readonly INewsService _newsService;

		public UtnNoticiasService(INewsService newsService)
		{
			_newsService = newsService;
		}
		public async Task<ICollection<NewsDto>> Search(string query)
		{
			//TODO: falta registrar los tiempos de acceso de la API
			var news = await _newsService.GetNewsAsync(query);

			return ObjectMapper.Map<ICollection<ArticleDto>, ICollection<NewsDto>>(news);
		}
	}
}
