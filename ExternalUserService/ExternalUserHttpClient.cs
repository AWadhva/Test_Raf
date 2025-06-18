using System.Net;
using System.Text.Json;
using ExternalUserService.Models;

public class ExternalUserHttpClient : IExternalUserClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public const string ClientId = "reqresClient";
    public ExternalUserHttpClient(IHttpClientFactory httpClientFactory)
    {
        if (httpClientFactory == null)
            throw new ArgumentException("Unexpected: httpClientFactory is null");

        _httpClientFactory = httpClientFactory;
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient(ClientId);
        client.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");
        return client;
    }
    
    public async Task<Result<User>> GetUserByIdAsync(int userId)
    {
        var client = CreateClient();

        try
        {
            var response = await client.GetAsync($"users/{userId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return Result<User>.Success(null);
            }
        
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            
            var parsed = JsonSerializer.Deserialize<UserResponse>(responseBody);
            return Result<User>.Success(parsed.Data);
        }
        catch (Exception exp)
        {
            return Result<User>.Failure(exp);
        }
    }

    // TODO: return MayBe
    public async Task<(int? TotalPages, List<User> Users)> GetUsersByPageAsync(int page)
    {
        int? TotalPages = -1;
        var client = CreateClient();

        // TODO: handle exception
        var response = await client.GetAsync($"users?page={page}");

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        // TODO: handle json exception
        var parsed = JsonSerializer.Deserialize<UserListResponse>(responseBody);        
        return (TotalPages: parsed?.TotalPages, parsed?.Data ?? new List<User>());
    }
}
