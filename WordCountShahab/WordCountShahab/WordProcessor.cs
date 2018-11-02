using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WordCountShahab
{
    public class WordProcessor
    {
        public void  CountWord(string filePath, string outFile)
        {
            Console.WriteLine("Counting words...");
            DateTime start_at = DateTime.Now;
            TrieNode root = new TrieNode(null, '?');
            Dictionary<DataReader, Thread> readers = new Dictionary<DataReader, Thread>();

            FileProcessor fp = new FileProcessor();
            fp.ChunkFileInto(filePath, 8);

            foreach (string path in fp.Files)
            {
                DataReader new_reader = new DataReader(path, ref root);
                Thread new_thread = new Thread(new_reader.ThreadRun);
                readers.Add(new_reader, new_thread);
                new_thread.Start();
            }

            foreach (Thread t in readers.Values) t.Join();

            DateTime stop_at = DateTime.Now;
            
            //List<TrieNode> top10_nodes = new List<TrieNode> { root, root, root, root, root, root, root, root, root, root };
            List<TrieNode> top10_nodes = new List<TrieNode> ();
            for (int i = 0; i < 100000; i++)
                top10_nodes.Add(root);
            int distinct_word_count = 0;
            int total_word_count = 0;
            
            root.GetTopCounts(ref top10_nodes, ref distinct_word_count, ref total_word_count);
            top10_nodes.Reverse();

            StringBuilder sb = new StringBuilder();
            foreach (TrieNode node in top10_nodes)
            {
                //Console.WriteLine("{0} - {1} times", node.ToString(), node.m_word_count);
                
                if (node.m_word_count != 0)
                {
                    sb.Append(String.Format("{0} - {1}", node.ToString(), node.m_word_count));
                    sb.Append("\n");
                }
                else
                    break;
            }

            using (StreamWriter writetext = new StreamWriter(outFile))
            {
                writetext.WriteLine(sb);
            }
        }
    }
}
