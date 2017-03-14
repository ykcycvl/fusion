using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;

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
}