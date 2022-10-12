using SplitBackDotnet.Dtos;
using SplitBackDotnet.Tests.Integration.Mocks;
using System.Net.Http.Json;
using SplitBackDotnet.Helper;
using System.Net;

namespace SplitBackDotnet.Tests.Integration;

public class AuthenticationTest : IClassFixture<TestWebApplicationFactory<Program>> {

  private readonly TestWebApplicationFactory<Program> _factory;
  private readonly HttpClient _httpClient;

  public AuthenticationTest(TestWebApplicationFactory<Program> factory) {
    _factory = factory;
    _httpClient = factory.CreateClient();
  }

  private readonly static UserCreateDto newUser = new() {
    Nickname = "test",
    Email = "test@email.com"
  };

  [Fact]
  public async Task SignUp_Test() {
    
    var response1 = await _httpClient.PostAsJsonAsync("/auth/request-sign-up", newUser);
    //response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string>? value);
    var stringInResponse = await response1.Content.ReadAsStringAsync();
    var tokenInEmail = stringInResponse.Remove(stringInResponse.Length - 1, 1).Remove(0, 1);

    await _httpClient.PostAsJsonAsync("/auth/verify-token", new TokenBody { Token = tokenInEmail });

    var response2 = await _httpClient.PostAsync("/auth/sign-in", null);
    
    Assert.Equal(HttpStatusCode.OK, response2.StatusCode);
  }
}
