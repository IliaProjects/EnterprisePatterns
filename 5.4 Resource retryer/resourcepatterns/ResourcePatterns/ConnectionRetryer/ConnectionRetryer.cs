using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePatterns.ConnectionRetryer
{
    internal class ConnectionRetryer : IConnection
    {
        IConnection _connection;

        private string _connRetryingString = "Connection error. Trying again...";
        private string _connUnavailableString = "Connection unavailable. Try again later.";
        private int _attempts = 5;

        public ConnectionRetryer (IConnection connection)
        {
            _connection = connection;
        }

        public void Open()
        {
            bool done = false;
            int attempts = _attempts;
            while (attempts-- >= 0 || done == false)
            {
                try
                {
                    _connection.Open();
                    done = true;
                }
                catch (SqlException)
                {
                    Console.WriteLine(_connRetryingString);
                }
            }
            if(done == false)
            {
                Console.WriteLine(_connUnavailableString);
            }
        }
        public void ExecuteQuery()
        {
            bool done = false;
            int attempts = _attempts;
            while (attempts-- >= 0 || done == false)
            {
                try
                {
                    _connection.ExecuteQuery();
                    done = true;
                }
                catch (SqlException)
                {
                    Console.WriteLine(_connRetryingString);
                }
            }
            if (done == false)
            {
                Console.WriteLine(_connUnavailableString);
            }
        }
        public void Close()
        {
            _connection.Close();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
