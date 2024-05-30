using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    
    public class Search
    {
        public void Run()
        {
            var index = new InvertedIndex();
            index.Add("the brown fox jumped over the brown dog", 1);
            index.Add("the lazy brown dog sat in the corner", 2);
            index.Add("the red fox bit the lazy dog", 3);
            index.Add("the red fox ran away", 4);
            index.Add("the lazy cat ran away", 5);
            index.Add("the lazy bear sat in the sun", 6);

            Console.WriteLine("Search for 'brown': " + string.Join(", ", index.Search("brown")));
            Console.WriteLine("Search for 'fox': " + string.Join(", ", index.Search("fox")));
            Console.WriteLine("Search for 'dog': " + string.Join(", ", index.Search("dog")));
            Console.WriteLine($"total documents is {index.totalDocuments}");
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Please enter a word to search.");
                string search = Console.ReadLine();
                List<string> searchResults = index.Search(search);
                Console.WriteLine("Search for '" + search + ":'");
                Console.WriteLine("Search for " + search + " was in index: " + string.Join(", ", index.Search(search)));
                foreach (var documentId in searchResults)
                {
                    Console.WriteLine($"Document {documentId}: {index.GetDocumentText(documentId)}");
                }



                Console.WriteLine("Do you want to search again? (y/n)");
                string response = Console.ReadLine();
                if (response.ToLower() != "y")
                {
                    isRunning = false;
                }
            }
        }
    }
}
