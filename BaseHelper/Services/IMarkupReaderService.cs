using System.Collections.Generic;
using System.Windows.Controls;

namespace BaseHelper.Services
{
    public interface IMarkupReaderService
    {
        IEnumerable<TreeViewItem> GetTreeViewItem(string fileName);
    }
}