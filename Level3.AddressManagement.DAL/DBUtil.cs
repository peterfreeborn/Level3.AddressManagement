using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace Level3.AddressManagement.DAL
{
    public class DBSafeConvert
    {
        public static String ToStringN(object value)
        {
            return (Convert.IsDBNull(value) || value == null) ? null : Convert.ToString(value);
        }

        public static Int32? ToInt32N(object value)
        {
            return (Convert.IsDBNull(value) || value == null || String.IsNullOrEmpty(value.ToString())) ? (Int32?)null : Convert.ToInt32(value);
        }

        public static byte[] ToByteArray(object value)
        {
            return (Convert.IsDBNull(value) || value == null || String.IsNullOrEmpty(value.ToString())) ? new byte[0] : (byte[])(value);
        }

        public static Double? ToDoubleN(object value)
        {
            return (Convert.IsDBNull(value) || value == null || String.IsNullOrEmpty(value.ToString())) ? (Double?)null : Convert.ToDouble(value);
        }

        public static DateTime? ToDateTimeUtcN(object value)
        {
            return (Convert.IsDBNull(value) || value == null || String.IsNullOrEmpty(value.ToString())) ? (DateTime?)null : DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Utc);
        }

        public static DateTime? ToDateTimeLocalN(object value)
        {
            return (Convert.IsDBNull(value) || value == null || String.IsNullOrEmpty(value.ToString())) ? (DateTime?)null : DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Local);
        }

        public static DateTime? ToDateTimeLocalNDotNetPrecision(object value)
        {
            return (Convert.IsDBNull(value) || value == null || String.IsNullOrEmpty(((DateTime)value).ToString("yyyy/MM/dd HH:mm:ss"))) ? (DateTime?)null : DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Local);
        }

        public static Boolean? ToBooleanN(object value)
        {
            if (Convert.IsDBNull(value) || value == null || String.IsNullOrEmpty(value.ToString()))
                return (Boolean?)null;

            if (value.ToString() == "Y")
                return true;

            if (value.ToString() == "N")
                return false;

            return Convert.ToBoolean(value);
        }

        public static String FromBooleanN(Boolean? value)
        {
            String retVal = String.Empty;

            if (value.HasValue)
            {
                retVal = value == true ? "Y" : "N";
            }

            return retVal;
        }
    }

    public interface ILoadDataRow
    {
        void AssignLocalVariables(DataRow dr);
    }

    public class DBUtil
    {
        /// <summary>
        /// Converts a DataTable to a List of objects of type T, populating the properties of each
        /// object T with values from the columns of the DataTable.
        /// </summary>
        /// <typeparam name="T">The type of the business objects to be constructed</typeparam>
        /// <param name="dataTable">The data from which the business objects will be constructed</param>
        /// <returns>A List of objects of type T whose properties correspond to the data in the DataTable</returns>
        public static List<T> DataTableToList<T>(DataTable dataTable) where T : ILoadDataRow, new()
        {
            if (dataTable == null) return null;

            T o = default(T);
            List<T> lt = new List<T>();

            foreach (DataRow dr in dataTable.Rows)
            {
                o = new T();

                o.AssignLocalVariables(dr);
                lt.Add(o);
            }

            return lt;
        }

        /// <summary>
        /// Constructs an object of type T using the values of the first row
        /// of the DataTable.
        /// </summary>
        /// <typeparam name="T">The type of the business object to be constructed</typeparam>
        /// <param name="dataTable">The data from which the business object will be constructed</param>
        /// <returns>An instance of a business object of type T whose property values correspond to the values in the first row of the DataTable</returns>
        public static T DataTableToObject<T>(DataTable dataTable) where T : ILoadDataRow, new()
        {
            if (dataTable == null || dataTable.Rows.Count <= 0) return default(T);

            return DBUtil.DataRowToObject<T>(dataTable.Rows[0]);
        }

        /// <summary>
        /// Constructs an object of type T using the provided DataRow.
        /// </summary>
        /// <typeparam name="T">The type of the business object to be constructed</typeparam>
        /// <param name="dataRow">The data from which the business object will be constructed</param>
        /// <returns>An instance of a business object of type T whose property values correspond to the values in the DataRow</returns>
        public static T DataRowToObject<T>(DataRow dataRow) where T : ILoadDataRow, new()
        {
            if (dataRow == null) return default(T);

            T o = new T();
            o.AssignLocalVariables(dataRow);

            return o;
        }

        /// <summary>
        /// Converts a DataRow to a Dictionary, with the column names as keys.
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> DataRowToDictionary(DataRow dr)
        {
            if (dr == null) return null;

            Dictionary<String, Object> dict = new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase);

            DataTable dt = dr.Table;
            foreach (DataColumn col in dt.Columns)
            {
                dict.Add(col.ColumnName, dr[col]);
            }

            return dict;
        }

        /// <summary>
        /// Converts the first row of a DataTable to a Dictionary, with the column names as keys.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> DataRowToDictionary(DataTable dt)
        {
            if (dt == null) return null;

            if (dt.Rows.Count < 0) return null;

            return DBUtil.DataRowToDictionary(dt.Rows[0]);
        }
    }
}
