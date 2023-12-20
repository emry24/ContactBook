using System.Diagnostics;
using Shared.Models;
using Shared.Models.Responses;
using Newtonsoft.Json;
using Shared.Interfaces;

namespace Shared.Services;

public class PersonService
{
    private readonly FileService _fileService = new FileService(Path.Combine(Environment.CurrentDirectory, "content.json"));
    private List<PersonalDataModel> _persons = new List<PersonalDataModel>();

    /// <summary>
    /// Adds person to list
    /// </summary>
    /// <param name="person">person of type PersonalDataModel</param>
    /// <returns>Returns response if successful or false if person already exists or fails</returns>
    public ServiceResult AddToList(PersonalDataModel person)
    {
        var response = new ServiceResult();
        try
        {

            if (!_persons.Any(x => x.Email == person.Email))
            {
                _persons.Add(person);

                var existingContent = _fileService.GetContentFromFile();
                var existingPersons = JsonConvert.DeserializeObject<List<PersonalDataModel>>(existingContent) ?? new List<PersonalDataModel>();
                existingPersons.Add(person);

                _fileService.SaveContentToFile(JsonConvert.SerializeObject(existingPersons));
                response.Status = Enums.ServiceStatus.SUCCEEDED;

            }
            else
            {
                response.Status = Enums.ServiceStatus.ALREADY_EXISTS;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
        }
        return response;
    }


    /// <summary>
    /// Delete personfrom list
    /// </summary>
    /// <param name="email">Deletes person by email</param>
    /// <returns>Returns true if person is successfully removed or false if person do not exist</returns>
    public bool DeletePerson(string email)
    {
        try
        {
            var personToDelete = _persons.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (personToDelete != null)
            {
                _persons.Remove(personToDelete);
                _fileService.DeleteContentFromFile(JsonConvert.SerializeObject(_persons));
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    /// <summary>
    /// Shows all persons from list
    /// </summary>
    /// <returns>Returns persons if list is not null or empty</returns>
    public List<PersonalDataModel> GetPersonsFromList()
    {
        try
        {
            var content = _fileService.GetContentFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                _persons = JsonConvert.DeserializeObject<List<PersonalDataModel>>(content)!;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return _persons;
    }

    /// <summary>
    /// Shows specific details of person in list
    /// </summary>
    /// <param name="email">Shows person based on email</param>
    /// <returns>Returns person if found in list</returns>

    public PersonalDataModel GetPersonByEmail(string email)
    {

        return _persons.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase))!;
    }


}
