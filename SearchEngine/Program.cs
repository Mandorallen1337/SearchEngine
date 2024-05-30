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
            
            var searchEngine = new Search();
            searchEngine.InitializeIndex(index);
            searchEngine.Run(index);

        }
    }
}
