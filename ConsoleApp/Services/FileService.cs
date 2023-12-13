using ConsoleApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp.Services;

//public interface IFileService
//{
//    bool SaveContentToFile(string content);
//    string GetContentFromFile();
//    bool DeleteContentFromFile(string content);
//    string GetPersonFromFile();
//}

internal class FileService(string filePath)
{
    private readonly string _filePath = filePath;

    public bool SaveContentToFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public bool DeleteContentFromFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }


    public string GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (var sr = new StreamReader(_filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }



    public string GetPersonFromFile(string email)
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (var sr = new StreamReader(_filePath))
                {
                    var content = sr.ReadToEnd();
                    var persons = JsonConvert.DeserializeObject<List<PersonalDataModel>>(content);

                    var person = persons?.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                    if (person != null)
                    {
                        return JsonConvert.SerializeObject(person);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return null!;

    }

}
