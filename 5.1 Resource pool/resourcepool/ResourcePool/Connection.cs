using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePool
{
    public class Connection : IConnection
    {
        private string _connectionString;
        private bool _opened;
        public Connection()
        {
            /*using (StreamReader r = new StreamReader("Config.json"))
            {
                _connectionString = JObject.Parse(r.ReadToEnd())["ConnectionStrings"]["DefaultConnection"].Value<string>();
            }*/
            _opened = false;
        }
        public void Open()
        {
            if (!_opened)
            {
                _opened = true;
                Console.WriteLine("Opened new connection");
            }
            else
            {
                Console.WriteLine("Connection already opened");
            }
        }
        public void ExecuteQuery()
        {
            Console.WriteLine("Query executed");
        }
        public void Close()
        {
            if (_opened)
            {
                _opened = false;
                Console.WriteLine("Connection closed");
            }
            else
            {
                Console.WriteLine("Connection already closed");
            }
        }
        public void Dispose()
        {
            Close();
        }
    }
}
