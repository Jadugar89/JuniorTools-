using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupReader
{
    public class XML_Object : AbstractMarkupObject<XML_Object>
    {
        
        
        public Dictionary<string,string>? Atribute;
        public XML_Object() : base()
        {

            Atribute = new Dictionary<string,string>();
        }

        


    }
}
