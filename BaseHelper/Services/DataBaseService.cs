using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.Services
{
    public class DataBaseService
    {
        private readonly string? conString;

        public DataBaseService(IConfiguration configuration)
        {
            conString = configuration.GetConnectionString("ConnectionString");
        }
    }

}
