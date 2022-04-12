using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiValueDictionary.Helpers;
using MultiValueDictionary.src.CommandManager;

namespace MultiValueDictionary
{
    public class StartSpreetailApplication : IHostedService
    {
        private IServiceProvider _serviceProvider;
        public StartSpreetailApplication(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start Spreetail");
            //var commandManager = new CommandManager();
            using var serviceScope = _serviceProvider.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var commandManager = provider.GetRequiredService<ICommandManager>();
            Console.WriteLine("The Multi-Value Dictionary app is a command line application that stores a multivalue dictionary in memory");
            Console.WriteLine("Please enter the commnds in the list:\nKEYS, MEMBERS, ADD, REMOVE, REMOVEALL, CLEAR, KEYIFEXISTS, MEMBEREXISTS, ITEMS");
            Console.WriteLine("To quit. Enter STOP/stop");
            bool quitFlag = false;
            while (!quitFlag)
            {
                var arguments = Console.ReadLine();
                if (arguments.ToUpper() == "STOP")
                {
                    quitFlag = true;
                    return;
                }
                //To validate inputs entered by user
                var validate = ValidateInputs.ValidateInput(arguments);

                if (!validate.IsValid)
                {
                    Console.WriteLine("Invalid inputs, Please enter valid inputs");
                    continue;
                }
                var result = commandManager.MultiValueDictionaryOperation(validate.Command, validate.Key, validate.Value);
                Console.WriteLine($"Message: {result.Message}\nOutputValues:\n{result.OutputValue}\nIsSuccess: {result.IsSuccess} ");
                Console.WriteLine("\n");
            }


        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            return;
        }
    }
}
