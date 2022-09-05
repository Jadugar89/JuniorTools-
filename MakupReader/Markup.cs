using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupReader
{
    public class Markup
    {
        private readonly string _filePath;

        public Markup(string filePath)
        {
            _filePath = filePath;
        }

        public ArrayList ReadMarkupFile()
        {
            FileInfo file = new FileInfo(_filePath);
            return new CreatorMarkup(file).CreateMarkup();
        }
    }
}
