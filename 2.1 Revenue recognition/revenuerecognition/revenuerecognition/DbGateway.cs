using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using revenuerecognition.Model;

namespace revenuerecognition
{
    internal class DbGateway
    {
        public Order FindOrder(long id)
        {
            throw new NotImplementedException();
        }
        public List<Recognition> FindRecognitions(long orderId)
        {
            throw new NotImplementedException();
        }
        public void InsertRecognition(long orderId, int sum, DateTime recognitionDate)
        {
            throw new NotImplementedException();
        }
    }
}
