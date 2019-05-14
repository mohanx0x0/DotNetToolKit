using System.Data;
using System.Globalization;

namespace DotNetToolKit.Extensions
{
    public static class Extensions
    {
        public static string IndianString(this decimal number)
        {
            return number.ToString("#,#.##", new CultureInfo(0x0439));
        }
        public static string IndianString(this int number)
        {
            return number.ToString("#,#", new CultureInfo(0x0439));
        }
        public static string IndianString(this decimal? number)
        {
            return number.ToString();
        }
        public static string IndianString(this int? number)
        {
            return number.ToString();
        }

        public static void RemoveColumn(this DataTable table, string columnName)
        {
            DataColumnCollection columns = table.Columns;
            if (columns.Contains(columnName))
                table.Columns.Remove(columnName);
        }
    }
}
