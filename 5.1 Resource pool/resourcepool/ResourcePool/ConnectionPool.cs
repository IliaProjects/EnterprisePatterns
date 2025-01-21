using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePool
{
    public class ConnectionPool
    {
        private static ConnectionPool _instance;
        public static ConnectionPool getInstance()
        {
            if (_instance == null)
                _instance = new ConnectionPool();
            return _instance;
        }

        private static int defaultConnections = 10;
        private static int maxConnections = 50;

        private Dictionary<int, IConnection> _availableConnections;
        private Dictionary<int, IConnection> _usedConnections;
        private ConnectionPool()
        {
            _availableConnections = new Dictionary<int, IConnection>();
            _usedConnections = new Dictionary<int, IConnection>();

            for(int i = 0; i < defaultConnections; i++)
            {
                openNewConnection(i + 1);
            }
        }
        private void openNewConnection(int key)
        {
            Connection connection = new Connection();
            connection.Open();
            _availableConnections.Add(key, new PooledConnection(connection, key));
            Console.WriteLine($"PooledConnection opened. Key = {key}");
        }

        /// 
        /// Public methods
        /// 
        public IConnection getConnection()
        {
            if(_availableConnections.Count < 1)
            {
                int total = _availableConnections.Count + _usedConnections.Count;
                if (total >= maxConnections)
                {
                    throw new Exception("Too many database connections");
                }
                int highestKeyValue = _availableConnections.Keys.Max();
                if (highestKeyValue < _usedConnections.Keys.Max())
                    highestKeyValue = _usedConnections.Keys.Max();

                openNewConnection(highestKeyValue + 1);
            }
            
            int key = _availableConnections.FirstOrDefault().Key;
            IConnection connection = _availableConnections[key];

            _usedConnections.Add(key, connection);
            _availableConnections.Remove(key);

            return connection;
        }

        public void putConnection(int key)
        {
            var connection = _usedConnections[key];
            _availableConnections.Add(key, connection);
            _usedConnections.Remove(key);
        }

        public void restoreConnection(int key)
        {
            if (!_availableConnections.Remove(key))
                _usedConnections.Remove(key);
        }
    }
}
