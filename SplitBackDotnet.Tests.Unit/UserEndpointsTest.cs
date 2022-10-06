using Microsoft.AspNetCore.Http.HttpResults;
using SplitBackDotnet.Endpoints;
using SplitBackDotnet.Models;
using SplitBackDotnet.Dtos;
using UnitTests.Helpers;
using AutoMapper;

namespace SplitBackDotnet.Tests.Unit; 

public class UserEndpointsTest {

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

  //[Fact]
  //public async Task AddUser_ShouldAddUserAndReturnUsers()
  //{

  //  //Arrange
  //  await using var context = new MockDb().CreateDbContext();
  //  var newUser = new UserCreateDto {
  //    Nickname = "blabla",
  //    Email = "blabla@email.com"
  //  };
  //  var mapper = new Mapper();

  //  //Act
  //  var result = await UserEndpoints.AddUser(context, mapper, newUser);

  //  //Assert
  //  Assert.IsType<Ok<List<User>>>(result);
  //}
}
