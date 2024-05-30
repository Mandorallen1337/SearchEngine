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

        public void Add(string word, int documentId)
        {
            totalDocuments++;                        
            // split the word into terms
            string[] terms = word.ToLower().Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            documentLengths[documentId.ToString()] = terms.Length;
            documentTexts[documentId.ToString()] = word;
            
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
                index[term].Add(new DocumentFreqency(documentId.ToString(), frequency));
            }            

        }

        public List<string> Search(string query)
        {
            string[] queryItems = query.ToLower().Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var documentScores = new Dictionary<string, double>();

            foreach(var term in queryItems)
            {
                if (index.ContainsKey(term))
                {
                    foreach (var documentFrequency in index[term])
                    {
                        string documentId = documentFrequency.DocId;
                        int frequency = documentFrequency.Frequency;
                        // calculate the tf-idf score
                        double tf = (double)frequency / documentLengths[documentId];
                        // calculate the idf score
                        double idf = Math.Log((double)totalDocuments / index[term].Count);
                        double tfIdf = tf * idf;

                        if (!documentScores.ContainsKey(documentId))
                        {
                            documentScores[documentId] = 0;                            
                        }
                        documentScores[documentId] += tfIdf;
                        
                    }
                }
            }
            // sort the documents by score
            var sortedDocumtents = documentScores.OrderByDescending(x => x.Value).Select(d => d.Key).ToList();
            return sortedDocumtents;
            
        }

        public string GetDocumentText(string docId)
        {
            return documentTexts[docId]; // Retrieve the full text of the document
        }



    }
       
}
