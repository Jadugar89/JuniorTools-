using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BaseHelper.Services
{
    public interface ITableService
    {
        ObservableCollection<object> GetTable(IEnumerable<string> columns, IEnumerable<IEnumerable<string>> data);
    }
}