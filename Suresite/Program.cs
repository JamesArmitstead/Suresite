using System;
using System.Linq;

/*
 * This program checks whether a 16 digit number follows a set of rules.
 */
namespace Suresite
{
    class Program
    {
        private const String InitialMessage = "Please input a 16 digit number or type exit to quit.";
        private const String FailureMessage = "Fail! The number is invalid.";
        private const String SuccessMessage = "Success! The number is valid.";

        static void Main(string[] args)
        {
            // Loop forever to allow the checking of multiple numbers without restarting the program
            String inputString;
            while (true)
            {
                Console.WriteLine(InitialMessage);
                inputString = Console.ReadLine();
                CardNumber cardNum = new CardNumber(inputString);
                if (cardNum.IsValid())
                {
                    Console.WriteLine(SuccessMessage + "\n\n");
                }
                else
                {
                    Console.WriteLine(FailureMessage + "\n\n");
                }
            }
        }
    }

    class CardNumber
    {
        private String number;
        private const String LengthMessage = "The inputted number is the wrong length.";
        private const String NumericMessage = "The inputted number contains characters that are not digits.";
        private const String SumMessage = "The sum of the digits obtained from summing the four digit sections are not equal.";
        private const String OddDigitsMessage = "The inputted number does not start and end with an odd digit.";

        public CardNumber(String inputtedNumber)
        {
            this.number = inputtedNumber;
        }

        // This method checks that the card number is valid
        public bool IsValid()
        {
            int[] sums = new int[4];

            // Check that the length of the number is 16
            if (number.Length != 16)
            {
                Console.WriteLine(LengthMessage);
                return false;
            }

            char currentChar;
            // Loop through each character of the number
            for(int i = 0; i < number.Length; i++)
            {
                currentChar = number.ElementAt(i);
                // Check that each character is a digit
                if (!Char.IsDigit(currentChar))
                {
                    Console.WriteLine(NumericMessage);
                    return false;
                }

                // Sum each of the four sets of digits
                sums[i / 4] = sums[i / 4] + int.Parse(currentChar.ToString());
            }

            // Loop through the sums of the four digit sections
            for (int i = 0; i < 4; i++)
            {
                String sumString = sums[i].ToString();
                int numDigits = sumString.Length;
                // Each sum can be either a one or two digit number
                if (numDigits == 2)
                {
                    // Sum the two digits
                    sums[i] = int.Parse(sumString.Substring(0, 1)) + int.Parse(sumString.Substring(1, 1));
                }
                else
                {
                    sums[i] = int.Parse(sumString);
                }
            }

            // Check that all the sums are the equal
            if (!sums.All(i => i.Equals(sums[0])))
            {
                Console.WriteLine(SumMessage);
                return false;
            }

            int firstDigit = int.Parse(number.ElementAt(0).ToString());
            int lastDigit = int.Parse(number.ElementAt(15).ToString());
            // Check that the first and last digits are odd
            if (firstDigit % 2 == 0 || lastDigit % 2 == 0)
            {
                Console.WriteLine(OddDigitsMessage);
                return false;
            }

            return true;
        }
    }
}
