using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.Models
{
    public class TableData
    {
        private string tableName;
        public List<string> row { get; set; }
        public string TableName { get { return tableName; } set { tableName = value; } }


        public TableData()
        {
            this.row = new List<string>();
        } 

    }
}
