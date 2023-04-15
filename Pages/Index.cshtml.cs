using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace dotnetcoresample.Pages;

public class IndexModel : PageModel
{

    public string OSVersion { get { return RuntimeInformation.OSDescription; }  }

    public string SecretValue { get; set; }
    
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        SecretValue = "Connection to Key Vault not established";

        var kvName = Environment.GetEnvironmentVariable("kvName");
        if(kvName != null)
        {
            try
            {
                var client = new SecretClient(new Uri($"https://{kvName}.vault.azure.net/"), new DefaultAzureCredential());
                var secret = client.GetSecret("supersecret");
                SecretValue = secret.Value.Value;
            }
            catch
            {
                
            }
        }
        
        _logger = logger;
    }

    public void OnGet()
    {        
    }
}
