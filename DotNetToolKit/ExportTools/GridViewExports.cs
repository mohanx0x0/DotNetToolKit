using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNetToolKit.ExportTools
{
    public static class GridViewExports
    {
        static object LockMulti = new object();
        public static DataTable GridViewToDataTable(GridView sourceGrid)
        {
            lock (LockMulti)
            {
                DataTable destinationTable = new DataTable();
                for (int i = 0; i < sourceGrid.Columns.Count; i++)
                    destinationTable.Columns.Add(sourceGrid.Columns[i].ToString());

                foreach (GridViewRow row in sourceGrid.Rows)
                {
                    DataRow dr = destinationTable.NewRow();
                    for (int j = 0; j < sourceGrid.Columns.Count; j++)
                        if (!row.Cells[j].Text.Equals("&nbsp;"))
                            dr[sourceGrid.Columns[j].ToString()] = row.Cells[j].Text;

                    destinationTable.Rows.Add(dr);
                }

                return destinationTable;
            }
        }

        public static void GridViewToExcel(GridView sourceGrid, string fileName)
        {
            lock (LockMulti)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    sourceGrid.AllowPaging = false;
                    sourceGrid.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in sourceGrid.HeaderRow.Cells)
                    {
                        cell.BackColor = sourceGrid.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in sourceGrid.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                                cell.BackColor = sourceGrid.AlternatingRowStyle.BackColor;
                            else
                                cell.BackColor = sourceGrid.RowStyle.BackColor;
                            cell.CssClass = "textmode";
                        }
                    }
                    sourceGrid.RenderControl(hw);
                    HttpContext.Current.Response.Output.Write(sw.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }
    }


}
