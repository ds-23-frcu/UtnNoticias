using Volo.Abp.Modularity;

namespace UtnNoticias;

[DependsOn(
    typeof(UtnNoticiasApplicationModule),
    typeof(UtnNoticiasDomainTestModule)
    )]
public class UtnNoticiasApplicationTestModule : AbpModule
{

}
