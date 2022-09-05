using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSQL._DbProviderFactory
{
    public class GMDbFactory : DbProviderFactory
    {
        private static GMDbFactory _GMDbFactory;
        

        public static GMDbFactory Instance
        {
            get { 
                if(_GMDbFactory == null)
                {
                    _GMDbFactory=  new GMDbFactory();
                }

                return _GMDbFactory; 
                }
           
        }


        protected GMDbFactory()
        {

        }

        public override bool CanCreateDataSourceEnumerator => base.CanCreateDataSourceEnumerator;


    }
}
