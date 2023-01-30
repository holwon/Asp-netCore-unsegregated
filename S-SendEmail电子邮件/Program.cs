// // See https://aka.ms/new-console-template for more information
// using FluentEmail.Core;

// Console.WriteLine("Hello, World!");
// var email = await Email
//     .From("hol.won@qq.com")
//     .To("hol.won@qq.com", "Luke")
//     .Subject("Hi Luke!")
//     .SetFrom("hol.won@qq.com")
//     .Body("Fluent email looks great!")
//     .SendAsync();

using System;

using EmailValidation;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            do
            {
                Console.Write("Enter an email address: ");

                var input = Console.ReadLine();
                if (input == null)
                    break;

                input = input.Trim();
                Console.WriteLine("{0} is {1}!", input, EmailValidator.Validate(input) ? "valid" : "invalid");
            } while (true);

            Console.WriteLine();
        }
    }
}