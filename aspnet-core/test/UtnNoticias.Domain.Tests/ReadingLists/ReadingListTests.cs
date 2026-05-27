using System;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace UtnNoticias.ReadingLists;

public class ReadingListTests
{
	[Fact]
	public void Should_Not_Create_List_With_Empty_Name()
	{
		var exception = Should.Throw<BusinessException>(() =>
			new ReadingList(Guid.NewGuid(), Guid.NewGuid(), " ")
		);

		exception.Code.ShouldBe(UtnNoticiasDomainErrorCodes.ReadingListNameIsRequired);
	}

	[Fact]
	public void Should_Not_Add_Duplicated_Item_Url()
	{
		var list = new ReadingList(Guid.NewGuid(), Guid.NewGuid(), "Lecturas");
		list.AddItem(
			"title-1",
			"https://news.example/item-1",
			null,
			null,
			null,
			null,
			null
		);

		var exception = Should.Throw<BusinessException>(() =>
			list.AddItem(
				"title-2",
				" https://news.example/item-1 ",
				null,
				null,
				null,
				null,
				null
			)
		);

		exception.Code.ShouldBe(UtnNoticiasDomainErrorCodes.ReadingListItemAlreadyExists);
	}
}

