using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VideoStoreApp
{
    class DTOReaderAndConnection
    {
        public SqlDataReader Reader { get; set; }
        public SqlConnection Connection { get; set; }
    }
}
