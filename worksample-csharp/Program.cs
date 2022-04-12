using MultiValueDictionary.Helpers;
using MultiValueDictionary.src.CommandManager;
using MultiValueDictionary.src.Enums;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiValueDictionary.src.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MultiValueDictionary
{
    class Program
    {
        private static readonly ConcurrentDictionary<string, List<string>> _readWriteDictionary = new ConcurrentDictionary<string, List<string>>();

        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                    services.AddTransient<ICommandManager>(c => new CommandManager(new MultiValueReadDictionaryService(_readWriteDictionary), new MultiValueWriteDictionaryService(_readWriteDictionary)))
                        .AddTransient<IMultiValueDataReadDictionary, MultiValueReadDictionaryService>()
                        .AddTransient<IMultiValueDataWriteDictionary, MultiValueWriteDictionaryService>()
                        .AddSingleton<IHostedService, StartSpreetailApplication>()); ;

        }
    }
}
