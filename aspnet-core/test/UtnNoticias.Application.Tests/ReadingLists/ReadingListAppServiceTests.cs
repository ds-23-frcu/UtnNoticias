using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using UtnNoticias.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Xunit;

namespace UtnNoticias.ReadingLists;

public class ReadingListAppServiceTests : UtnNoticiasApplicationTestBase
{
	private readonly IReadingListAppService _readingListAppService;
	private readonly IRepository<ReadingList, Guid> _readingListRepository;
	private readonly IDbContextProvider<UtnNoticiasDbContext> _dbContextProvider;

	public ReadingListAppServiceTests()
	{
		_readingListAppService = GetRequiredService<IReadingListAppService>();
		_readingListRepository = GetRequiredService<IRepository<ReadingList, Guid>>();
		_dbContextProvider = GetRequiredService<IDbContextProvider<UtnNoticiasDbContext>>();
	}

	[Fact]
	public async Task Should_Create_Update_And_Delete_Reading_List()
	{
		var created = await _readingListAppService.CreateAsync(new CreateReadingListDto
		{
			Name = "Lista inicial"
		});

		created.Id.ShouldNotBe(Guid.Empty);
		created.Name.ShouldBe("Lista inicial");

		var updated = await _readingListAppService.UpdateAsync(created.Id, new UpdateReadingListDto
		{
			Name = "Lista actualizada"
		});

		updated.Name.ShouldBe("Lista actualizada");

		await _readingListAppService.AddItemAsync(created.Id, new AddReadingListItemDto
		{
			Title = "Item to delete",
			Url = "https://news.example/item-delete"
		});

		await _readingListAppService.DeleteAsync(created.Id);

		var dbContext = await _dbContextProvider.GetDbContextAsync();
		dbContext.ReadingLists.Any(x => x.Id == created.Id).ShouldBeFalse();
		dbContext.ReadingListItems.Any(x => x.ReadingListId == created.Id).ShouldBeFalse();
	}

	[Fact]
	public async Task Should_Add_Item_And_Block_Duplicated_Url()
	{
		var created = await _readingListAppService.CreateAsync(new CreateReadingListDto
		{
			Name = "Lista con noticias"
		});

		var withItem = await _readingListAppService.AddItemAsync(created.Id, new AddReadingListItemDto
		{
			Title = "News title",
			Url = "https://news.example/item-1",
			Author = "Author"
		});

		withItem.Items.Count.ShouldBe(1);

		var exception = await Should.ThrowAsync<BusinessException>(async () =>
		{
			await _readingListAppService.AddItemAsync(created.Id, new AddReadingListItemDto
			{
				Title = "Another title",
				Url = " https://news.example/item-1 "
			});
		});

		exception.Code.ShouldBe(UtnNoticiasDomainErrorCodes.ReadingListItemAlreadyExists);
	}

	[Fact]
	public async Task Should_Not_Allow_Operating_Another_User_List()
	{
		var foreignListId = Guid.NewGuid();

		var foreignList = new ReadingList(
			foreignListId,
			Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
			"Foreign list"
		);

		await _readingListRepository.InsertAsync(foreignList, autoSave: true);

		await Should.ThrowAsync<EntityNotFoundException>(async () =>
		{
			await _readingListAppService.UpdateAsync(foreignListId, new UpdateReadingListDto
			{
				Name = "Attempted update"
			});
		});

		await Should.ThrowAsync<EntityNotFoundException>(async () =>
		{
			await _readingListAppService.AddItemAsync(foreignListId, new AddReadingListItemDto
			{
				Title = "Blocked item",
				Url = "https://news.example/blocked"
			});
		});
	}

	[Fact]
	public async Task Should_List_Only_Current_User_Reading_Lists()
	{
		await _readingListAppService.CreateAsync(new CreateReadingListDto
		{
			Name = "Own list"
		});

		var foreignList = new ReadingList(
			Guid.NewGuid(),
			Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
			"Foreign list for query"
		);
		await _readingListRepository.InsertAsync(foreignList, autoSave: true);

		var ownLists = await _readingListAppService.GetListAsync();

		ownLists.Any(x => x.OwnerId == Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")).ShouldBeFalse();
	}
}
