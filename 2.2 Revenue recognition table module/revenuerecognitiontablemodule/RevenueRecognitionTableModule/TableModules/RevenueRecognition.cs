using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueRecognitionTableModule.TableModules
{
    public class RevenueRecognition : ModuleBase
    {
        public string orderIdColumn = "orderId";
        public string revenueColumn = "revenue";
        public string recognitionTimeColumn = "recognitionTime";

        public RevenueRecognition(string connectionString) : base(connectionString, "RevenueRecognitions")
        {

        }
        public int GetOrderId(int id)
        {
            return GetColumn<int>(id, orderIdColumn);
        }
        public int GetRevenue(int id)
        {
            return GetColumn<int>(id, revenueColumn);
        }
        public DateTime GetRecognitionTime(int id)
        {
            return GetColumn<DateTime>(id, recognitionTimeColumn);
        }
        public void InsertRecognition(DataRow row)
        {
            InsertRow(row);
        }
        public DataRow newRecognitionRow()
        {
            DataTable dt = GetTable();
            dt.Columns.Remove("id");
            return dt.NewRow();
        }
    }
}
