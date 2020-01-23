/*
 * ITSE 1430
 * Frank Rygiewicz
 */
 using System;

namespace section1
{
    class Program
    {
        static void Main ( string[] args )
        {
            //PlayingWithVariables();
            AddMovie();

            string title = "";

            int releaseYear = 1990;

            int runLength = 192;

            string description = "";
        }

        static void AddMovie ()
        {
            string title = ReadString("Enter a title: ", true);

            int releaseYear = ReadInt32("Enter the release year (>= 0): ", 0, 2100);
            int runLength = ReadInt32("Enter the run length (>= 0): ", 0, 86400);

            string description = ReadString("Enter a description: ", false); ;
            bool isClassic = ReadBoolean("Is this a classic movie?");
        }

        private static bool ReadBoolean ( string message )
        {
            Console.Write(message + " (Y/N)");
            string value = Console.ReadLine();

            //TODO: Do this correctly?
            char firstChar = value[0];
            return firstChar == 'Y' || firstChar == 'y';
        }

        private static string ReadString (string message, bool required)
        {
            Console.Write(message);
            string value = Console.ReadLine();

            //TODO: Validate
            return value;
        }

        private static int ReadInt32 (string message, int minValue, int maxValue)
        {
            Console.Write(message);
            string temp = Console.ReadLine();
            //int value = Int32.Parse(temp);

            //TODO: Clean this up
            int value;
            if (Int32.TryParse(temp, out value))
                return value;

            //TODO: Validate input
            return -1;
        }

        private static void PlayingWithVariables ()
        {
            Console.WriteLine("Hello World!");

            int hours;
            double payRate;
            string name;
            bool pass;

            /*int newHours = hours;*/ //Won't compile because hours has not been defined
            hours = 10;
            int newHours2 = hours;

            //Logical block 1
            int hours2 = 10;
            int hours3; //Don't do this
            hours3 = 3; //Don't so this

            Console.WriteLine("Enter a value"); // Displays a message to the console window for the user to see
            Console.WriteLine(10); //Overloaded the WriteLine function to display an int
            string input = Console.ReadLine();  // Gets input from the user

            //int results; //Don't do this
            //results = Foo(); //Don't do this
            //int results2 = Foo();

            int x, y, z;
            int a = 10, b = 20, c = 30;
        }
    }
}
