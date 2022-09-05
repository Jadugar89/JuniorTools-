using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupReader.Model
{
    internal class XmlConfigurationListCreator : IConfigurationListCreator
    {
        public string MarkupStart { get; set; } = "<";
        public string MarkupStartEnd { get; set; } = ">";
        public string MarkupEndStart { get; set; } = "</";
        public string MarkupEnd { get; set; } = "/>";

    }
}
