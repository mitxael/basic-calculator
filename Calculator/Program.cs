/*
    Project:    Calculator

    Release 1.0 <by Foobar's HR>:
         - Addition of two numbers
    Release 2.0 <by Michael Vasquez Otazu>:
         - Added validation on input
         - Added more operations (subtraction, multiplication and division)
*/

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        private static Boolean successfulInput;

        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                Console.WriteLine("***Welcome to Calculator v2.0*** \n");

                // Choose the operation to be performed
                int operation = SelectOperation();

                // Get the numbers
                int[] argumentsArray = GetInput(args, operation);
                int firstArgument = argumentsArray[0];
                int secondArgument = argumentsArray[1];
                
                // Perform the operation and show the result
                if(successfulInput)
                {
                    int result = PerformOperation(operation, firstArgument, secondArgument);
                    Console.WriteLine("Result: " + result);
                }
            }
            else
            {
                Console.WriteLine("calculator.exe needs one of the following parameters:\n " +
                                  "\t interactive : numbers are get from user's keyboard input.\n" +
                                  "\t <filename>: numbers are get from user's specified file (full path).");
            }

            // Wait and quit
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
        }

        static int[] GetInput(string[] args, int operation)
        {
            //Declare variables
            int[] argumentsArray = new int[2];
            ref int firstArgument = ref argumentsArray[0];
            ref int secondArgument = ref argumentsArray[1];

            // Interactive mode (read from keyboard input)
            if (args[0] == "interactive")
            {
                Console.WriteLine("Enter <first number>: ");
                while (!int.TryParse(Console.ReadLine(), out firstArgument))
                {
                    Console.WriteLine("Error: invalid integer.\nEnter <first number>: ");
                }

                Console.WriteLine("Enter <second number>: ");
                while ( (!int.TryParse(Console.ReadLine(), out secondArgument)) || (secondArgument == 0 && operation == 4) )
                {
                    if(secondArgument == 0 && operation == 4)
                        Console.WriteLine("Error: division by zero.\nEnter <second number>: ");
                    else
                        Console.WriteLine("Error: invalid integer.\nEnter <second number>: ");
                }
            }
            // Static mode (read from file)
            else
            {
                try
                {
                    successfulInput = true;
                    string readAllText = File.ReadAllText(@args[0]);
                    var strings = readAllText.Split("\r\n".ToCharArray());
                    /// Possible improvement: remove spaces

                    // Try to read integers, and update the status of "inputIsValid" based on the result
                    successfulInput &= (int.TryParse(strings[0], out firstArgument));
                    successfulInput &= (int.TryParse(strings[1], out secondArgument));
                    Console.WriteLine("Error. number(s) read from file are not correct.");
                }
                catch (Exception ex)
                {
                    successfulInput = false;
                    Console.WriteLine("Error. " + ex.Message);
                }
            }

            return argumentsArray;
        }

        static int SelectOperation()
        {
            // Declare local variables
            int operation;

            // Enter the number related to the operation to be performed
            Console.WriteLine("Available operations: \n" +
                              "\t1 : Addition (a+b) \n" +
                              "\t2 : Subtraction (a-b) \n" +
                              "\t3 : Multiplication (a*b) \n" +
                              "\t4 : Division (a/b) \n" +
                              "Enter <operation>:");
            while ( (!int.TryParse(Console.ReadLine(), out operation)) || (operation < 1) || (operation > 4) )
            {
                Console.WriteLine("Error: invalid operation!\nOperation: ");
            }

            return operation;
        }

        static int PerformOperation(int operation, int firstArgument, int secondArgument)
        {
            // Declare local variables
            int result;

            // Perform the selected operation
            switch (operation)
            {
                case 1:
                    result = firstArgument + secondArgument;
                    break;
                case 2:
                    result = firstArgument - secondArgument;
                    break;
                case 3:
                    result = firstArgument * secondArgument;
                    break;
                case 4:
                    result = firstArgument / secondArgument;
                    break;
                default:
                    result = 0;
                    break;
            }

            return result;
        }
    }
}

