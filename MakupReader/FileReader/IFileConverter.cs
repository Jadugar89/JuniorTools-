using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupReader.FileReader
{
    internal interface IFileConverter
    {
        Task<List<string>> ConvertToList(FileInfo file);
    }
}
