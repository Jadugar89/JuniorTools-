using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupReader
{
    public class XmlObject : AbstractMarkupObject<XmlObject>
    {
        
        
        public Dictionary<string,string>? Atribute;
        public XmlObject() : base()
        {

            Atribute = new Dictionary<string,string>();
        }

        


    }
}
