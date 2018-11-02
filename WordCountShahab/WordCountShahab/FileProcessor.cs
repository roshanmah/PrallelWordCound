using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCountShahab
{
    public class FileProcessor
    {
        public IList<string> Files { get; set; }
        public void ChunkFileInto(string filePath,int size)
        {
            int lines = File.ReadAllLines(filePath).Length;
            int chunksize = Convert.ToInt32(lines / size) + 1;


            string[] ss = File.ReadAllLines(filePath);

            int cycle = 1;
           

            var chunk = ss.Take(chunksize);
            var rem = ss.Skip(chunksize);
            string pathOnly = Path.GetDirectoryName(filePath);
            Files = new List<string>();

            while (chunk.Take(1).Count() > 0)
            {
                string filename = pathOnly+"\\myFileSB" + cycle.ToString() + ".txt";
                Files.Add(filename);
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    foreach (string line in chunk)
                    {
                        sw.WriteLine(line);
                    }
                }
                chunk = rem.Take(chunksize);
                rem = rem.Skip(chunksize);
                cycle++;
            }

            

        }

    }
}
