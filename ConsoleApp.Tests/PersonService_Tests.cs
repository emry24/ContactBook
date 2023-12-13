
using Shared.Models;
using Shared.Services;

namespace ConsoleApp.Tests;

public class PersonService_Tests
{
    [Fact]
    public void AddToListShould_AddOnePersonToList_ThenReturnTrue()
    {
        //Arrange
        PersonalDataModel person = new PersonalDataModel { FirstName = "", LastName = "", StreetName = "", PostalCode = "", City = "", Email = "", Phone = "" };
        var personService = new PersonService();

        //Act
        var result = personService.AddToList(person);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(Shared.Enums.ServiceStatus.SUCCEEDED, result.Status);
    }
}
