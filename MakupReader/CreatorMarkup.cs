using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkupReader.FileReader;
using MarkupReader.Model;

namespace MarkupReader
{
    internal class CreatorMarkup 
    {

        private readonly FileInfo _file;

        internal CreatorMarkup(FileInfo file)
        {
            _file = file;
        }

        internal ArrayList CreateMarkup()
        {
            switch (_file.Extension.ToLower())
            {
                case ".xml":
                    return new ArrayList(new ListCreator<XmlObject>(new XMLConverter(), 
                                         _file, new XmlConfigurationListCreator()).
                                         createMarkupObj());
                case ".json":
                    return null;
                default:
                   return null;

            }
        }
    }
}
