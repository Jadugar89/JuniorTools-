using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupReader
{
    public abstract class AbstractMarkupObject<T>
    {
        public string Name { get; set; }
        public IList<T> Children;
        public string? Text { get; set; }

        public AbstractMarkupObject()
        {
            
            Children = new List<T>();
        }

    }
}
