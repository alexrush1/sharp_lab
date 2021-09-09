using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nsu.timofeev.first_lab.Movers;
using nsu.timofeev.sharpLab.Movers;
using nsu.timofeev.sharpLab.NameGenerator;
using nsu.timofeev.sharpLab.OutputWriter;

namespace nsu.timofeev.sharpLab
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
                    collection.AddScoped<IFoodGenerator, FoodGenerator>();
                    collection.AddScoped<IOutputWriter>(ctx => new OutputFileWriter("log.txt"));
                    collection.AddScoped<INameGenerator, NameGenerator.NameGenerator>();
                    collection.AddScoped<IWormMover, CloseFoodMover>();
                });
        }
    }
}