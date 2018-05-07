using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }

    public static string GetUserProperty(string accountName, string propertyName)
    {
        if (string.IsNullOrEmpty(accountName))
            return "";

        DirectoryEntry entry = new DirectoryEntry();
        entry.Path = "LDAP://fg.local:389/DC=fg,DC=local";
        entry.AuthenticationType = AuthenticationTypes.Secure;

        DirectorySearcher search = new DirectorySearcher(entry);
        search.Filter = "(SAMAccountName=" + accountName + ")";
        search.PropertiesToLoad.Add(propertyName);

        SearchResultCollection results = search.FindAll();
        if (results != null && results.Count > 0)
        {
            return results[0].Properties[propertyName][0].ToString();
        }
        else
        {
            return "Unknown User";
        }
    }

    public static bool IsMemberOf(string accountName, string GroupName)
    {
        if (string.IsNullOrEmpty(accountName))
            return false;

        PrincipalContext context = new PrincipalContext(ContextType.Domain, "fg.local");
        UserPrincipal upUser = UserPrincipal.FindByIdentity(context, accountName);
        if (upUser != null)
            return upUser.IsMemberOf(context, IdentityType.SamAccountName, GroupName);
        else
            return false;
    }
}