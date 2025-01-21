using ResourcePool;
using ResourcePool.ResourceTimer;

IConnection connection = new Connection();
IConnection timedConnection = new TimedConnection(connection);
timedConnection.Open();
int x = Convert.ToInt32(Console.ReadLine());
if (x == 0)
{
    timedConnection.Close();
}

Convert.ToInt32(Console.ReadLine());