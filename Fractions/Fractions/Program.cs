namespace Fractions
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string message = @"Welcome to mini calculator!
            .1 Legal operators shall be *, /, +, - (multiply, divide, add, subtract)
            .2 Operands and operators shall be separated by one or more spaces
            .3  Mixed numbers will be represented by whole_numerator/denominator. e.g. 3_1 / 4
            .4 Improper fractions and whole numbers are also allowed as operands";

            Console.WriteLine(message);

            string[] input = new string[0];
            do
            {
                try
                {
                    Console.Write("? ");
                    input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string s = string.Join(",", null);
                    if (input.Length >= 3)
                    {
                        Evaluator evaluator = new Evaluator(input);
                        Console.WriteLine("= " + evaluator.Evaluate());
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Ensure the input is properly formatted according to the given instruction above.");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (input.Length > 0);            
        }
    }
}
