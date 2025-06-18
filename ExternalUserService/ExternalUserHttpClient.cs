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
    
    public async Task<Result<(int TotalPages, List<User> Users)>> GetUsersByPageAsync(int page)
    {        
        var client = CreateClient();

        try
        {
            var response = await client.GetAsync($"users?page={page}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return Result<(int TotalPages, List<User> Users)>.Success((-1, null));
            }

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var parsed = JsonSerializer.Deserialize<UserListResponse>(responseBody);
            return Result<(int TotalPages, List<User> Users)>.Success((TotalPages: parsed.TotalPages, parsed.Data));
        }
        catch (Exception exp)
        {
            return Result<(int, List<User>)>.Failure(exp);
        }
    }
}
