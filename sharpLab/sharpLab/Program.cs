using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nsu.timofeev.first_lab.Movers;
using nsu.timofeev.sharpLab;
using nsu.timofeev.sharpLab.Movers;
using nsu.timofeev.sharpLab.OutputWriter;
using sharpLab.Database;
using System.Configuration;
using sharpLab.FoodGenerator;
using sharpLab.Movers;

namespace sharpLab
{
    class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, collection) =>
                {
                    collection.AddHostedService<WorldService>();
                    collection.AddScoped<IFoodGenerator, NonRandomFoodGenerator>();
                    collection.AddScoped<IOutputWriter>(ctx => new OutputFileWriter("log.txt"));
                    collection.AddScoped<INameGenerator, NameGenerator>();
                    collection.AddScoped<IWormMover, HttpPostMover>();
                    collection.AddScoped<IDatabaseFoodReader, DatabaseFoodReader>();
                    collection.AddScoped<IDatabaseFoodLoader, DatabaseFoodLoader>();
                    collection.AddDbContextPool<DatabaseContext>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["WormDB"].ConnectionString));
                });
        }
    }
}