using ConsoleApp.Interfaces;
using ConsoleApp.Models;
namespace ConsoleApp.Services;


public class MenuService : IMenuService
{
    private readonly PersonService _personService = new PersonService();


    public void ShowMainMenu()
    {
        while (true)
        {
            DisplayMenuTitle("MENU OPTIONS");
            Console.WriteLine($"{"1.",-4} Add New Contact");
            Console.WriteLine($"{"2.",-4} Wiew Contact List");
            Console.WriteLine($"{"3.",-4} Wiew Contact Details");
            Console.WriteLine($"{"4.",-4} Delete Contact");
            Console.WriteLine($"{"0.",-4} Exit Application");
            Console.WriteLine();
            Console.Write("Enter Menu Option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowAddPersonOption();
                    break;
                case "2":
                    ShowViewPersonListOption();
                    break;
                case "3":
                    ShowPersonDetailOption();
                    break;
                case "4":
                    ShowDeletePersonOption();
                    break;
                case "0":
                    ShowExitApplicationOption();
                    break;
                default:
                    Console.WriteLine("\nInvalid option selected. Press any key to continue.");
                    Console.ReadKey();
                    break;

            }

        }

    }
    private void ShowAddPersonOption()
    {
        PersonalDataModel personalData = new PersonalDataModel();


        DisplayMenuTitle("Add New Contact");

        Console.Write("First Name: ");
        personalData.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        personalData.LastName = Console.ReadLine()!;

        Console.Write("Street Name: ");
        personalData.StreetName = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        personalData.PostalCode = Console.ReadLine()!;

        Console.Write("City: ");
        personalData.City = Console.ReadLine()!;

        Console.Write("E-mail: ");
        personalData.Email = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        personalData.Phone = Console.ReadLine()!;

        _personService.AddToList(personalData);

    }

    private void ShowDeletePersonOption()
    {
        DisplayMenuTitle("Delete Contact");

        Console.Write("Enter the email of the contact to delete: ");
        var emailToDelete = Console.ReadLine();

        if (_personService.DeletePerson(emailToDelete!))
        {
            Console.WriteLine("Contact deleted successfully. Press any key to continue.");
        }
        else
        {
            Console.WriteLine("Contact not found. Press any key to continue.");
        }

        Console.ReadKey();

    }

    private void ShowViewPersonListOption()
    {
        var persons = _personService.GetPersonsFromList();



        DisplayMenuTitle("Contact List");

        foreach (var item in persons)
        {
            Console.WriteLine($"{item.FirstName} {item.Email}");
        }

        Console.ReadKey();
    }

    private void ShowPersonDetailOption()
    {
        DisplayMenuTitle("View Contact Details");

        Console.Write("Enter the email of the contact to view details: ");
        var emailToView = Console.ReadLine();

        var person = _personService.GetPersonByEmail(emailToView!);

        if (person != null)
        {
            Console.WriteLine($"Name: {person.FirstName} {person.LastName}");
            Console.WriteLine($"Street: {person.StreetName}");
            Console.WriteLine($"Postal Code: {person.PostalCode}");
            Console.WriteLine($"City: {person.City}");
            Console.WriteLine($"Email: {person.Email}");
            Console.WriteLine($"Phone: {person.Phone}");
        }
        else
        {
            Console.WriteLine("Contact not found. Press any key to continue.");
        }

        Console.ReadKey();
    }



    private void ShowExitApplicationOption()
    {
        Console.Clear();
        Console.WriteLine("Are you sure you want to close this application? (y/n): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            Environment.Exit(0);
    }

    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"## {title} ##");
        Console.WriteLine();
    }


}
