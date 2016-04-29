using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class SqlValidation
    {


        public static object DbNullIfNull(this object obj)
        {
            return obj != null ? obj : DBNull.Value;
        }

        public static object DbNullIfNullOrEmpty(this string str)
        {
            return !String.IsNullOrEmpty(str) ? str : DBNull.Value.ToString();
        }

        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string name)
        {
            var col = reader.GetOrdinal(name);
            return reader.IsDBNull(col) ?
                        (DateTime?)null :
                        (DateTime?)reader.GetDateTime(col);
        }



    }
}
