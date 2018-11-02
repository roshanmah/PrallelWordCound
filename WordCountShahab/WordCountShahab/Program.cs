using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCountShahab
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFile = "";
            string outFile = "";
            if (args.Length < 2)
            {
                Console.WriteLine("You Missed The Arguments!!");

                //inFile = "H:\\GHDS\\wordlist.txt";
                //outFile = "H:\\GHDS\\a.txt";
                Environment.Exit(0);
            }
            else
            {
                inFile = args[0];
                outFile = args[1];
            }
            WordProcessor wp = new WordProcessor();
            wp.CountWord(inFile,outFile);
            
        }
    }
}
