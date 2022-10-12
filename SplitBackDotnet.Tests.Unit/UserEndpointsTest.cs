using Microsoft.AspNetCore.Http.HttpResults;
using SplitBackDotnet.Endpoints;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Profiles;
using UnitTests.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace SplitBackDotnet.Tests.Unit;

public class UserEndpointsTests {

  [Fact]
  public async Task GetUsers_ShouldReturnUsers() {

    //Arrange
    await using var context = new MockDb().CreateDbContext();
    context.Add(new User {
      Nickname = "blabla",
      Email = "blabla@email.com"
    });
    await context.SaveChangesAsync();

    //Act
    var result = await UserEndpoints.GetUsers(context);

    //Assert
    Assert.IsType<Ok<List<User>>>(result);
  }

  [Fact]
  public async Task AddUser_ShouldReturnAllUsersWithNewUser_WhenSentAUser() {

    //Arrange
    var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
    var mapper = config.CreateMapper();

    await using var context = new MockDb().CreateDbContext();
    var newUser = new UserCreateDto {
      Nickname = "blabla",
      Email = "blabla@email.com"
    };

    //Act
    var actualResult = (Ok<List<User>>) await UserEndpoints.AddUser(context, mapper, newUser);
    var responseList = new List<User> {
      new User {
        Id = 1,
        Nickname = "blabla",
        Email = "blabla@email.com"
      }
    };
    var expectedResult = Results.Ok(responseList);

    //Assert
    //Assert.Equal(((Ok<List<User>>?)expectedResult)?.Value?[0].Email, actualResult.Value?[0].Email);
    //Assert.Equal(((Ok<List<User>>?)expectedResult)?.Value?[0].Nickname, actualResult.Value?[0].Nickname);
    Assert.IsType<Ok<List<User>>>(actualResult);
  }
}
