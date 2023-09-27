using UtnNoticias.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace UtnNoticias.Permissions;

public class UtnNoticiasPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UtnNoticiasPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(UtnNoticiasPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UtnNoticiasResource>(name);
    }
}
