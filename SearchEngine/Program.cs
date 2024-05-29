using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var index = new InvertedIndex();
            index.Add("the brown fox jumped over the brown dog", 1);
            index.Add("the lazy brown dog sat in the corner", 2);
            index.Add("the red fox bit the lazy dog", 3);

            Console.WriteLine("Please enter a word to search.");
            string search = Console.ReadLine();
            Console.WriteLine("Search for '" + search + "': " + string.Join(", ", index.Search(search)));
            Console.WriteLine("Search for 'brown': " + string.Join(", ", index.Search("brown")));
            Console.WriteLine("Search for 'fox': " + string.Join(", ", index.Search("fox")));
            Console.WriteLine("Search for 'dog': " + string.Join(", ", index.Search("dog")));
            Console.WriteLine($"total documents is {index.totalDocuments}");
            Console.ReadLine();

        }
    }
}
