using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine(" User Information ");
        Console.WriteLine();

        Console.Write("Enter your first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter your last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter your birth date (yyyy-MM-dd): ");
        string birthDateInput = Console.ReadLine();

        Console.Write("Enter your national code: ");
        string nationalCode = Console.ReadLine();

        Console.Write("Enter your mobile number: ");
        string mobile = Console.ReadLine();

        Console.Write("Enter your bank card number: ");
        string cardNumber = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine(" Validation Results ");

        mobile = CleanMobileNumber(mobile);

        if (IsValidMobile(mobile))
        {
            Console.WriteLine("Mobile number: " + mobile);
        }
        else
        {
            Console.WriteLine("Mobile number is not valid.");
        }

        if (IsValidNationalCode(nationalCode))
        {
            Console.WriteLine("National code is valid.");
        }
        else
        {
            Console.WriteLine("National code is not valid.");
        }

        if (DateTime.TryParse(birthDateInput, out DateTime birthDate))
        {
            int age = CalculateAge(birthDate);
            Console.WriteLine("Age: " + age);
        }
        else
        {
            Console.WriteLine("Birth date is not valid.");
        }

        string bankName = GetBankName(cardNumber);

        Console.WriteLine("Bank: " + bankName);

        Console.WriteLine();
        Console.WriteLine(" User Information ");
        Console.WriteLine("Name: " + firstName + " " + lastName);
        Console.WriteLine("Birth Date: " + birthDateInput);
        Console.WriteLine("National Code: " + nationalCode);
        Console.WriteLine("Mobile: " + mobile);
        Console.WriteLine("Card Number: " + cardNumber);

        Console.WriteLine();
        Console.WriteLine(" Mock Data");

        List<string> mockUsers = new List<string>
        {
            "Ali Ahmadi | 0499370899 | 09123456789",
            "Sara Mohammadi | 0079057853 | 09351234567",
            "Reza Karimi | 1234567891 | 09221234567"
        };

        foreach (string user in mockUsers)
        {
            Console.WriteLine(user);
        }
    }


    static string CleanMobileNumber(string mobile)
    {
        mobile = mobile.Replace(" ", "");
        mobile = mobile.Replace("-", "");

        if (mobile.StartsWith("+98"))
        {
            mobile = "0" + mobile.Substring(3);
        }
        else if (mobile.StartsWith("0098"))
        {
            mobile = "0" + mobile.Substring(4);
        }
        else if (mobile.Length == 10 && mobile.StartsWith("9"))
        {
            mobile = "0" + mobile;
        }

        return mobile;
    }


    static bool IsValidMobile(string mobile)
    {
        return Regex.IsMatch(mobile, @"^09\d{9}$");
    }


    static bool IsValidNationalCode(string nationalCode)
    {
        string pattern = @"^(?!([0-9])\1{9})\d{10}$";

        if (!Regex.IsMatch(nationalCode, pattern))
        {
            return false;
        }

        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += int.Parse(nationalCode[i].ToString()) * (10 - i);
        }

        int remainder = sum % 11;
        int checkDigit = int.Parse(nationalCode[9].ToString());

        if (remainder < 2)
        {
            return checkDigit == remainder;
        }
        else
        {
            return checkDigit == 11 - remainder;
        }
    }

    static int CalculateAge(DateTime birthDate)
    {
        DateTime today = DateTime.Today;

        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    static string GetBankName(string cardNumber)
    {
        cardNumber = cardNumber.Replace(" ", "");
        cardNumber = cardNumber.Replace("-", "");

        if (!Regex.IsMatch(cardNumber, @"^\d{16}$"))
        {
            return "Invalid card number";
        }

        string prefix = cardNumber.Substring(0, 6);

        Dictionary<string, string> banks = new Dictionary<string, string>()
        {
            { "603799", "Bank Melli" },
            { "589210", "Bank Sepah" },
            { "610433", "Bank Mellat" },
            { "621986", "Bank Saman" },
            { "627648", "Bank Tose'e Saderat" },
            { "603770", "Bank Keshavarzi" },
            { "603769", "Bank Saderat" },
            { "502229", "Bank Pasargad" },
            { "622106", "Bank Parsian" },
            { "627353", "Bank Tejarat" }
        };

        if (banks.ContainsKey(prefix))
        {
            return banks[prefix];
        }

        return "Unknown bank";
    }
}