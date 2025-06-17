using System.Net;
using System.Text.Json;
using ExternalUserService.Models;

public class ExternalUserHttpClient : IExternalUserClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ExternalUserHttpClient(IHttpClientFactory httpClientFactory)
    {
        if (httpClientFactory == null)
            throw new ArgumentException("Unexpected: httpClientFactory is null");

        _httpClientFactory = httpClientFactory;
    }

    private HttpClient CreateClient()
    {
        return _httpClientFactory.CreateClient("reqresClient");
    }

    // TODO: return MayBe
    public async Task<User> GetUserByIdAsync(int userId)
    {
        var client = CreateClient();

        // TODO: handle exception
        var response = await client.GetAsync($"users/{userId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        // TODO: handle json exception
        var parsed = JsonSerializer.Deserialize<UserResponse>(responseBody);
        return parsed?.Data;
    }

    // TODO: return MayBe
    public async Task<List<User>> GetUsersByPageAsync(int page)
    {
        var client = CreateClient();

        // TODO: handle exception
        var response = await client.GetAsync($"users?page={page}");

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        // TODO: handle json exception
        var parsed = JsonSerializer.Deserialize<UserListResponse>(responseBody);
        return parsed?.Data ?? new List<User>();
    }
}
