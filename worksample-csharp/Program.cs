using MultiValueDictionary.Helpers;
using MultiValueDictionary.src.CommandManager;
using MultiValueDictionary.src.Enums;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiValueDictionary.src.Services;

namespace MultiValueDictionary
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                    services.AddTransient<ICommandManager, CommandManager>()
                        .AddTransient<IMultiValueDataReadDictionary, MultiValueReadDictionaryService>()
                        .AddTransient<IMultiValueDataWriteDictionary, MultiValueWriteDictionaryService>()
                        .AddSingleton<IHostedService, StartSpreetailApplication>());

        }
    }
}
