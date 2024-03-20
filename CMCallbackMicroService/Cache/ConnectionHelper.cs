using Serilog;
using StackExchange.Redis;

namespace AccountManagementMicroService.Cache
{
    public class ConnectionHelper
    {
        
        static ConnectionHelper()
        {
            
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                IConfiguration _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings." + env + ".json")
                    .Build();

                String redis_con = _configuration["RedisURL"];
                String ip = redis_con.Split(':')[0];
                String port = redis_con.Split(':')[1];

                var config = new ConfigurationOptions()
                {
                    EndPoints = { { ip, Convert.ToInt32(port) } },
                    AbortOnConnectFail = false,
                    PreserveAsyncOrder = false,
                    KeepAlive = 180,
                    ConnectTimeout= 600000,
                    ConnectRetry=3,
                    SyncTimeout= 600000,
                    AllowAdmin=true,
                };
                Log.Debug($"preparing redis connection here {config}");
                Log.Information($"preparing redis connection here {config}");
                return ConnectionMultiplexer.Connect(config);
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
