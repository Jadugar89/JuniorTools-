using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupReader.FileReader
{
    internal class XMLConverter : IFileConverter
    {
        
        public async Task<List<string>> ConvertToList(FileInfo file)
        {
            Stream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            List<string>  vs = new List<string>();
            using (StreamReader reader = new StreamReader(stream))
            {
                StringBuilder line = new StringBuilder();

                bool start = false;
                // Read and display lines from the file until the end of
                // the file is reached.
                while (reader.Peek() >= 0)
                {
                    char c = (char)reader.Read();

                    if (CheckChar(c))
                    {
                        if (c == '<' && reader.Peek() != '?')
                        {
                            start = true;
                            if (!String.IsNullOrWhiteSpace(line.ToString()))
                            {
                                vs.Add(line.ToString());
                            }
                            line.Clear();
                        }

                        if (c == '/' && (reader.Peek() == '>'))
                        {
                            start = false;
                            vs.Add(line.ToString() + "/>");
                            line.Clear();
                        }
                        else
                        {
                            if (line.ToString().Contains("</") && c == '>')
                            {
                                start = false;
                                vs.Add(line.ToString() + ">");
                                line.Clear();
                            }
                            else
                            {
                                if (start && c == '>')
                                {
                                    vs.Add(line.ToString() + '>');
                                    line.Clear();
                                }
                                else
                                {
                                    if (start)
                                    {
                                        line.Append(c);
                                    }
                                }
                            }
                        }

                    }
                }
                return vs;
            }

        }
        private bool CheckChar(char c)
        {
            if (c == ' ')
            {
                return true;
            }
            else
            {
                return !char.IsWhiteSpace(c);
            }

        }
    }
}
