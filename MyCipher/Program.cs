using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyCipher
{
    class Program
    {
        //Plaintext Character Array
        private static char[] plaintext = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        //Ciphered Char decleration
        private static char[] cipher;

        static void Main(string[] args)
        {
            //Header & Setting up the Colors
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("//////////////");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("    Cipher");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("//////////////");
            Console.ForegroundColor = ConsoleColor.White;

            //New Line
            Console.WriteLine("\n");

            //Requests the text file
            Console.Write("Please enter the file you would like to use: ");
            //Reads the line
            String filename = Console.ReadLine();
            Console.WriteLine();
            //Instantiation of the lines array
            List<String> lines = new List<String>();
            //Instantiation of the output file
            StreamReader outputFile;

            try {
                //If the file does not exist, it creates one
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }
            else
            {
                //Set's the outputFile equal to the entered file name
                outputFile = File.OpenText(filename);
                //Put's all the lines in filename into the lines list
                while(!outputFile.EndOfStream)
                    {
                        lines.Add(outputFile.ReadLine());
                    }
            }
            } catch(Exception error)
            {
              Console.WriteLine("Error" + error.Message);
            }

            //Converts the keyword (removes all duplicates)
            String keyword = keywordConverter("AlexElguezabal");

            //Instantiates the cipher array
            cipher = new char[26];
            //For every letter in the alphabet (26 letters) a letter is added to ciper
            for(int i = 0; i < 26; i++)
            {
                //Adds all the chars from keyword into the cipher array
                if(i <= keyword.Length-1)
                {
                    cipher[i] = keyword[i];
                } else //Adds the rest of the charaters to fill the cipher array
                {
                    //Starting char
                    char holder = plaintext[i - keyword.Length];
                    //int for increasing to find the next char
                    int spaces = 0;
                    //If the cipher array already contains holder than it will go through the plaintext array until a new char is found
                    while (cipher.Contains(holder))
                    {
                        //Holder = plaintext i+the amount of increases - the amount that the keyword has added
                        holder = plaintext[(i+spaces++) - keyword.Length];
                    }
                    //Sets cipher to the holder
                    cipher[i] = holder;
                }
            }

            //Divider in the program
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + "Ciphered Text" + "");
            Console.ForegroundColor = ConsoleColor.White;

            
            foreach (String n in lines)
            {
                //Temp string
                String temp = n;
                //Stack of char(per line)
                List<char> stack = new List<char>();
                //Adds each char from the line to the stack until the line is empty
                do
                { 
                    //Adds each char from the line to the stack 
                    stack.Add(char.ToUpper(temp[0]));
                    //Removes the letter from the line
                    temp = temp.Substring(1);
                } while (!temp.Equals(""));

                //For every letter in the stack
                for(int i = 0; i < stack.Count; i++)
                {
                    //If the letter is not a letter
                    if (char.IsPunctuation(stack[i]) || stack[i].Equals(' '))
                        continue;
                    //Sets the letter to the converted letter
                    stack[i] = getCharFromChar(stack[i]);
                }

                //New line buffer
                Console.WriteLine();

                //Writes out the ciphered line
                foreach(char h in stack)
                {
                    Console.Write(h);
                }
            }
            
            //Ending text.
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        //Get's the ciphered char from a plaintext char
        public static char getCharFromChar(char character)
        {
            //Holding char decleration
            char ret = 'I';
            //Holder declaration
            int holder = -1;
            //Goes through the plaintext array until the corrisponding char is found
            for(int i = 0; i < plaintext.Length; i++)
            {
                //If the char is equal to the plaintext i
                if (character == plaintext[i])
                {
                    //the holder is set to i
                    holder = i;
                    break;
                }                
            }
            //returns the ciphered char from holder
            return cipher[holder];
        }

        //Removes all the duplicates from the keywords
        public static String keywordConverter(String word)
        {
            //Temp from the word
            String temp = word;
            //Makes the line uppercase
            temp = temp.ToUpper();
            //Runs for the length of the word
            for(int i = 0; i < word.Length; i++)
            {   try
                {
                    //Instantiaton of the letter
                    char letter = temp[i];
                    String holder = temp;
                    //Goes from the end of the string and removes all duplicates of the letter
                    while (temp.LastIndexOf(letter) != i)
                    {
                        temp = temp.Substring(0, temp.LastIndexOf(letter)) + temp.Substring(temp.LastIndexOf(letter) + 1);
                    }
                }catch(Exception ee)
                {
                }
            }
            //Returns the letter
            return temp;
        }
    } 

}
