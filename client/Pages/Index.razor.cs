using Microsoft.AspNetCore.Components;
using IdentityModel.Client;

namespace client.Pages;

public partial class Index
{
    [Inject]
    public IHttpClientFactory ClientFactory { get; set; }
    
    public string Content { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var identityClient = ClientFactory.CreateClient();
        var discDoc = await identityClient.GetDiscoveryDocumentAsync("https://localhost:7179");

        var tokenResult = await identityClient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest()
            {
                Address = discDoc.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "super_hard_to_guess",
                Scope = "Api_1"
            });
        
        var apiClient = ClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResult.AccessToken);
        var content = await apiClient.GetStringAsync("https://localhost:7246/secret");
        Content = content;

        StateHasChanged();
    }
}