using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace ConsoleApp.Services;

//public interface IPersonService
//{
//    ServiceResult AddToList(IPersonalData person);
//    ServiceResult DeletePerson(string email);
//    GetPersonsFromList();
//    GetPersonByEmail(string email);
//}

public class PersonService
{
    private readonly FileService _fileService = new FileService(Path.Combine(Environment.CurrentDirectory, "content.json"));
    private List<PersonalDataModel> _persons = new List<PersonalDataModel>();

    public ServiceResult AddToList(PersonalDataModel person)
    {
        var response = new ServiceResult();
        try
        {

            if (!_persons.Any(x => x.Email == person.Email))
            {
                _persons.Add(person);
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_persons));
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

    public ServiceResult AddToList(IPersonalData person)
    {
        throw new NotImplementedException();
    }

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



    public PersonalDataModel GetPersonByEmail(string email)
    {
        return _persons.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase))!;
    }


}
