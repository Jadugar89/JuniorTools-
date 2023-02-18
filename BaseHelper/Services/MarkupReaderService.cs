using BaseHelper.Utility;
using MarkupReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BaseHelper.Services
{
    public class MarkupReaderService : IMarkupReaderService
    {
        private TreeViewCreator treeViewCreator;

        public MarkupReaderService()
        {
            treeViewCreator = new TreeViewCreator();
        }

        public IEnumerable<TreeViewItem> GetTreeViewItem(string fileName)
        {
            var aMarkups = new Markup(fileName).ReadMarkupFile();
            if (aMarkups != null)
            {
                Type type = aMarkups[0].GetType();
                var lMarkups = aMarkups.Cast<XmlObject>().ToList();
                if (type == typeof(XmlObject))
                {
                    return treeViewCreator.AddItems<XmlObject>(lMarkups);
                }

            }
            return Enumerable.Empty<TreeViewItem>();
        }

    }
}
