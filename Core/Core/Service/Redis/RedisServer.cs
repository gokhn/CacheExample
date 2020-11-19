using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Redis
{
    public class RedisServer
    {
        /*
         ConnectionMultiplexer sınıfı bizlere Redis sunucusuna bağlanmayı, ilgili veri tabanını kullanmayı, bu veri tabanları üzerindeki işlemleri yapmamızı sağlayan metotları barındırmaktadır.
         */

        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationString;
        private int _currentDatabaseId = 0;

        public RedisServer(IConfiguration configuration)
        {
            CreateRedisConfigurationString(configuration);

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        public IDatabase Database => _database;

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(configurationString).FlushDatabase(_currentDatabaseId);
        }

        private void CreateRedisConfigurationString(IConfiguration configuration)
        {
            string host = configuration.GetSection("RedisConfiguration:Host").Value;
            string port = configuration.GetSection("RedisConfiguration:Port").Value;

            configurationString = $"{host}:{port}";
        }
    }
}
