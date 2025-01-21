using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePool.ResourceTimer
{
    internal class TimedConnection : IConnection
    {
        private class ConnTimer
        {
            private int _secondsLeft;
            private int _totalSeconds;
            private bool _timerStarted;
            private IConnection _connection;
            
            public ConnTimer(int seconds, IConnection connection)
            {
                _secondsLeft = seconds;
                _totalSeconds = seconds;
                _timerStarted = false;
                _connection = connection;
            }
            
            [MethodImpl(MethodImplOptions.Synchronized)]
            private int getSeconds()
            {
                return _secondsLeft;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            private void putSeconds(int seconds)
            {
                _secondsLeft = seconds;
            }
            private void onTimerStarted()
            {
                _timerStarted = true;
                reset();
            }

            //Время аренды полностью вышло
            private void onTimerStopped()
            {
                if (_timerStarted)
                {
                    _timerStarted = false;
                    _connection.Close();
                }
            }

            //IConnection.Close() был вызван клиентским кодом
            public void onConnectionClosed()
            {
                if (_timerStarted)
                {
                    _timerStarted = false;
                    putSeconds(0);
                }
            }
            public async Task start()
            {
                if (!_timerStarted)
                {
                    onTimerStarted();

                    while (getSeconds() > 0)
                    {
                        await Task.Delay(1000);
                        putSeconds(getSeconds() - 1);
                        Console.WriteLine(getSeconds());
                    }

                    onTimerStopped();
                }
            }
            public void reset()
            {
                putSeconds(_totalSeconds);
            }
        }

        private int _secondsBeforeClosing = 600;
        private IConnection _connection;
        private ConnTimer _timer;

        public TimedConnection(IConnection connection)
        {
            _connection = connection;
            _timer = new ConnTimer(_secondsBeforeClosing, this);
        }
        public void Open()
        {
            _timer.start();
            _connection.Open();
        }
        public void ExecuteQuery()
        {
            _timer.reset();
            _connection.ExecuteQuery();
        }
        public void Close()
        {
            _timer.onConnectionClosed();
            _connection.Close();
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
