using BaseHelper.Models;
using ClassMaker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.Services
{
    public class TableService : ITableService
    {
        private ManagerClassMaker classMaker;

        public TableService()
        {
            this.classMaker = new ManagerClassMaker();
        }
        public ObservableCollection<object> GetTable(IEnumerable<string> columns, IEnumerable<IEnumerable<string>> data)
        {

            Type dynType = classMaker.CreateTypeFromTable(columns);
            var listTable = new List<object>();
            foreach (var row in data)
            {
                var values = classMaker.AddDatatoFields(dynType, row.ToArray());
                if (values != null) listTable.Add(values);
            }
            return new ObservableCollection<object>(listTable);
        }

    }

}
