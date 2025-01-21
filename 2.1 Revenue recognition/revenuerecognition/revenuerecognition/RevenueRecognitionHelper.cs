using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace revenuerecognition
{
    internal class RevenueRecognitionHelper
    {
        DbGateway _dbGateway;

        public RevenueRecognitionHelper(DbGateway gateway)
        {
            _dbGateway = gateway;
        }

        public void CalculateRecognitions(long orderId)
        {
            var order = _dbGateway.FindOrder(orderId);
            int totalRevenue = order.totalRevenue;
            DateTime recognitionDate = order.OrderTime;
            string type = order.Type;

            if(type == "W") {
                _dbGateway.InsertRecognition(orderId, totalRevenue, recognitionDate);
            } else if (type == "S") {
                int oneThirdOfRevenue = totalRevenue / 3;
                _dbGateway.InsertRecognition(orderId, oneThirdOfRevenue, recognitionDate);
                _dbGateway.InsertRecognition(orderId, oneThirdOfRevenue, recognitionDate.AddDays(60));
                _dbGateway.InsertRecognition(orderId, oneThirdOfRevenue, recognitionDate.AddDays(90));
            } else if (type == "D") {
                int oneThirdOfRevenue = totalRevenue / 3;
                _dbGateway.InsertRecognition(orderId, oneThirdOfRevenue, recognitionDate);
                _dbGateway.InsertRecognition(orderId, oneThirdOfRevenue, recognitionDate.AddDays(30));
                _dbGateway.InsertRecognition(orderId, oneThirdOfRevenue, recognitionDate.AddDays(60));
            }
        }

        public int RecognizedRevenue(long orderId, DateTime asOf)
        {
            int result = 0;
            var recognitions = _dbGateway.FindRecognitions(orderId);
            foreach (var r in recognitions)
            {
                if (r.RecognitionDate <= asOf)
                    result += r.Revenue;
            }
            return result;
        }
    }
}
