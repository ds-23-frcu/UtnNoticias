using UtnNoticias.Themes;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace UtnNoticias;

public class UtnNoticiasTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
	private readonly IRepository<Theme, int> _themeRepository;
	private readonly IdentityUserManager _identityUserManager;


	public UtnNoticiasTestDataSeedContributor(IRepository<Theme, int> themeRepository, IdentityUserManager identityUserManager)
	{
		_themeRepository = themeRepository;
		_identityUserManager = identityUserManager;
	}

	public async Task SeedAsync(DataSeedContext context)
	{
		// Add users
		IdentityUser identityUser1 = new IdentityUser(Guid.NewGuid(), "test_user1", "testuser1@email.com");
		await _identityUserManager.CreateAsync(identityUser1, "1q2w3E*");
		await _identityUserManager.AddToRoleAsync(identityUser1, "Admin");

		await _themeRepository.InsertAsync(new Theme { Name = "Primer tema", User = identityUser1 });

		await _themeRepository.InsertAsync(new Theme { Name = "Segundo tema", User = identityUser1 });

		await _themeRepository.InsertAsync(new Theme { Name = "Tercer tema", User = identityUser1 });
	}
}
