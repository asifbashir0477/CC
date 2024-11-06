using System;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Step 1: Take inputs from the user
        Console.Write("Enter your registration number (e.g., 37123): ");
        string regNumber = Console.ReadLine();

        Console.Write("Enter your full name (e.g., Mohammad Asim): ");
        string fullName = Console.ReadLine();

        Console.Write("Enter your favorite movie (e.g., Oppenheimer): ");
        string favoriteMovie = Console.ReadLine();

        // Step 2: Extract required components
        string regDigits = ExtractRegNumber(regNumber);  // First two digits of reg. number
        string nameChars = ExtractSecondLetters(fullName);  // Second letters of first and last names
        string movieChars = ExtractMovieChars(favoriteMovie);  // Two characters from the movie

        // Step 3: Generate the remaining random characters
        string specialChars = "!@*%^&$";  // Special characters (excluding '#')
        Random random = new Random();
        string remainingChars = GenerateRandomString(8, random, specialChars);

        // Step 4: Combine all parts to form the password
        string password = regDigits + nameChars + movieChars + remainingChars;

        // Shuffle the password for randomness
        password = new string(password.OrderBy(c => random.Next()).ToArray());

        // Step 5: Display the generated password
        Console.WriteLine($"Generated Password: {password}");
    }

    // Function to extract the first two digits of the registration number
    static string ExtractRegNumber(string regNumber)
    {
        return regNumber.Length >= 2 ? regNumber.Substring(0, 2) : regNumber.PadRight(2, '0');
    }

    // Function to extract the second letters of the first and last names
    static string ExtractSecondLetters(string fullName)
    {
        var names = fullName.Split(' ');

        char firstNameSecond = names[0].Length >= 2 ? names[0][1] : 'x';  // Default 'x' for short names
        char lastNameSecond = names.Length > 1 && names[1].Length >= 2 ? names[1][1] : 'x';

        return $"{firstNameSecond}{lastNameSecond}".ToLower();
    }

    // Function to extract two characters from the movie title
    static string ExtractMovieChars(string movie)
    {
        return movie.Length >= 2 ? movie.Substring(0, 2).ToLower() : movie.PadRight(2, 'x');
    }

    // Function to generate a random string of given length
    static string GenerateRandomString(int length, Random random, string specialChars)
    {
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string allChars = allowedChars + specialChars;

        return new string(Enumerable.Repeat(allChars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
