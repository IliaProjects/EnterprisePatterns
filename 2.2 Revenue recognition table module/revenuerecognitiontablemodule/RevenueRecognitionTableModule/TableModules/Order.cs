using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueRecognitionTableModule.TableModules
{
    public class Order : ModuleBase
    {
        public string idColumn = "id";
        public string typeColumn = "type";
        public string orderTimeColumn = "orderTime";
        public string totalRevenueColumn = "totalRevenue";

        public Order(string connectionString) : base(connectionString, "Orders")
        {
        }

        public string GetType(int id)
        {
            return GetColumn<string>(id, typeColumn);
        }
        public DateTime GetOrderTime(int id)
        {
            return GetColumn<DateTime>(id, orderTimeColumn);
        }
        public int GetTotalRevenue(int id) 
        {
            return GetColumn<int>(id, totalRevenueColumn);
        }
        public void InsertOrder(DataRow row)
        {
            InsertRow(row);
        }

        public void InsertOrderWithRecognitions(DataRow orderRow)
        {
            string orderType = (string)orderRow[typeColumn];

            if (orderType != "W" && orderType != "S" && orderType != "D")
                return;

            int orderId = InsertRow(orderRow);
            RevenueRecognition recognition = new RevenueRecognition(_connectionString);
            if (orderType == "W")
            {
                DataRow row = recognition.newRecognitionRow();
                row[recognition.orderIdColumn] = orderId;
                row[recognition.revenueColumn] = orderRow[totalRevenueColumn];
                row[recognition.recognitionTimeColumn] = orderRow[orderTimeColumn];
                recognition.InsertRecognition(row);
            }
            else if (orderType == "S")
            {
                DataRow row = recognition.newRecognitionRow();
                row[recognition.orderIdColumn] = orderId;
                row[recognition.revenueColumn] = Convert.ToInt32(orderRow[totalRevenueColumn])/3;
                row[recognition.recognitionTimeColumn] = orderRow[orderTimeColumn];
                recognition.InsertRecognition(row);

                row[recognition.recognitionTimeColumn] = Convert.ToDateTime(orderRow[orderTimeColumn]).AddDays(60);
                recognition.InsertRecognition(row);

                row[recognition.recognitionTimeColumn] = Convert.ToDateTime(orderRow[orderTimeColumn]).AddDays(90);
                recognition.InsertRecognition(row);
            }
            else if (orderType == "D")
            {
                DataRow row = recognition.newRecognitionRow();
                row[recognition.orderIdColumn] = orderId;
                row[recognition.revenueColumn] = Convert.ToInt32(orderRow[totalRevenueColumn]) / 3;
                row[recognition.recognitionTimeColumn] = orderRow[orderTimeColumn];
                recognition.InsertRecognition(row);

                row[recognition.recognitionTimeColumn] = Convert.ToDateTime(orderRow[orderTimeColumn]).AddDays(30);
                recognition.InsertRecognition(row);

                row[recognition.recognitionTimeColumn] = Convert.ToDateTime(orderRow[orderTimeColumn]).AddDays(60);
                recognition.InsertRecognition(row);
            }
        }
    }
}
