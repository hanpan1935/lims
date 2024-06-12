using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Windows.Forms.DataFormats;

namespace Lanpuda.Lims.UI.Utils
{
    public class DataGridExportService : ServiceBase, IExportService
    {
        public void Export()
        {
            DataGrid grid = (DataGrid)AssociatedObject;
            
            //put grid ItemsSource to a DataTable
            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (var col in grid.Columns)
            {
                dt.Columns.Add(col.Header.ToString());
            }
            foreach (var item in grid.ItemsSource)
            {
                var row = dt.NewRow();
                foreach (var col in grid.Columns)
                {
                    var binding = (col as DataGridBoundColumn).Binding as System.Windows.Data.Binding;
                    var pathBinding = binding.Path.Path;
                    var value = item.GetType().GetProperty(pathBinding).GetValue(item, null);
                    row[col.Header.ToString()] = value;
                }
                dt.Rows.Add(row);
            }

            //export DataTable to csv
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns.Cast<System.Data.DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (System.Data.DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            string csv = sb.ToString();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "export.csv";
            string fullPath = Path.Combine(path, fileName);
            File.WriteAllText(fullPath, csv);
        }
    }


    public interface IExportService
    {
        void Export();
    }
}
