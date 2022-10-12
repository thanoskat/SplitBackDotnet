using SplitBackDotnet.Dtos;
using SplitBackDotnet.Tests.Integration.Mocks;
using System.Net.Http.Json;
using SplitBackDotnet.Helper;
using System.Net;
using Newtonsoft.Json;

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
    
    var requestSignUpResponse = await _httpClient.PostAsJsonAsync("/auth/request-sign-up", newUser);
    //response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string>? value);
    var stringInResponse = await requestSignUpResponse.Content.ReadAsStringAsync();
    var tokenInEmail = JsonConvert.DeserializeObject<string>(stringInResponse);

    await _httpClient.PostAsJsonAsync("/auth/verify-token", new TokenBody { Token = tokenInEmail });

    var signInResponse = await _httpClient.PostAsync("/auth/sign-in", null);

    Assert.Equal(HttpStatusCode.OK, signInResponse.StatusCode);
  }
}
