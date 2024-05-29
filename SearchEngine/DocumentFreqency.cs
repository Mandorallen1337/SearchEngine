using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class DocumentFreqency
    {
        public string DocId { get; set; }
        public int Frequency { get; set; }

        public DocumentFreqency(string docId, int frequency)
        {
            DocId = docId;
            Frequency = frequency;
        }
    }
}
