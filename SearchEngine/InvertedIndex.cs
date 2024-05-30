using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class InvertedIndex
    {
        public Dictionary<string, List<DocumentFreqency>> index;
        public Dictionary<string, int> documentLengths;
        public Dictionary<string, string> documentTexts;
        public int totalDocuments;

        public InvertedIndex()
        {
            index = new Dictionary<string, List<DocumentFreqency>>();
            documentLengths = new Dictionary<string, int>();
            documentTexts = new Dictionary<string, string>();
            totalDocuments = 0;
        }

        public void Add(string word, string documentId)
        {
            totalDocuments++;                        
            // split the word into terms
            string[] terms = word.ToLower().Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            documentLengths[documentId] = terms.Length;
            documentTexts[documentId] = word;
            
            var termFrequencies = new Dictionary<string, int>();
            // count the frequency of each term
            foreach (var term in terms)
            {
                if (!termFrequencies.ContainsKey(term))
                {
                    termFrequencies[term] = 0;
                }
                termFrequencies[term]++;

            }
            // add the term frequencies to the index
            foreach (var entry in termFrequencies)
            {
                string term = entry.Key;
                int frequency = entry.Value;

                if (!index.ContainsKey(term))
                {
                    index[term] = new List<DocumentFreqency>();
                }
                index[term].Add(new DocumentFreqency(documentId, frequency));
            }            

        }

        public List<string> Search(string query)
        {
            string[] queryItems = query.ToLower().Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var documentScores = new Dictionary<string, double>();

            foreach(var term in queryItems)
            {
                if (!index.ContainsKey(term))
                    continue;
                var postings = index[term];
                double idf = Math.Log((double)totalDocuments / postings.Count);
                foreach (var posting in postings)
                {
                    double tf = (double)posting.Frequency / documentLengths[posting.DocId];
                    double tfIdf = tf * idf;
                    if (!documentScores.ContainsKey(posting.DocId))
                    {
                        documentScores[posting.DocId] = 0.0;
                    }
                    documentScores[posting.DocId] += tfIdf;

                }                                
                
            }
            // sort the documents by score
            var sortedDocuments = documentScores.OrderByDescending(d => d.Value).Select(d => d.Key).ToList(); // Sort documents by score (descending)            
            return sortedDocuments;            
        }

        public string GetDocumentText(string docId)
        {
            return documentTexts[docId]; // Retrieve the full text of the document
        }



    }
       
}
