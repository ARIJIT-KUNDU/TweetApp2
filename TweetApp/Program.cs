using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseMetricsWebTracking()
            .UseMetrics(options=>{
                options.EndpointOptions = endPointOptions =>
                  {
                      endPointOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                      endPointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                      endPointOptions.EnvironmentInfoEndpointEnabled = false;
                  }; });
    }
}
