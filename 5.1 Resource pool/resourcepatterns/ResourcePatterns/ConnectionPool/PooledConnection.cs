using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePatterns.ConnectionPool
{
    public class PooledConnection : IConnection
    {
        private IConnection _connection;
        private int _key;
        public PooledConnection(IConnection connection, int key)
        {
            _connection = connection;
            _key = key;
        }
        public void Open()
        {
            _connection.Open();
        }
        public void ExecuteQuery()
        {
            _connection.ExecuteQuery();
        }
        public void Close()
        {
            ConnectionPool.getInstance().putConnection(_key);
        }
        public void Dispose()
        {
            ConnectionPool.getInstance().restoreConnection(_key);
            _connection.Dispose();
        }
    }
}
