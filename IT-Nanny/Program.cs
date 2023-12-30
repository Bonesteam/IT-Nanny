using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using IT_Nanny.Commands.TestModule;
using Microsoft.Extensions.DependencyInjection;

namespace IT_Nanny;

public class Program
{
    private const string Token = "MTE3OTgxOTQzNDUyNTg2ODE1Mg.GbYf3j.GTQ9Z-hoKw9ysbUhJHLa5Oh4gLl5gGwB5hMrn4";

    public static async Task Main(string[] args)
    {
        var client = new DiscordSocketClient();
        await client.LoginAsync(TokenType.Bot, Token);
        await client.StartAsync();
        client.Log += async message =>
        {
            Console.WriteLine(message);
            await Task.CompletedTask;
        };
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IDiscordClient, DiscordSocketClient>(s => client)
            .BuildServiceProvider();
        
        var interactionService = new InteractionService(client.Rest);
        await interactionService.AddModuleAsync<TestModule>(serviceProvider);
        
        client.SlashCommandExecuted += async (command) =>
        {
            var interactionContext = new InteractionContext(client, command);
            await interactionService.ExecuteCommandAsync(interactionContext, serviceProvider);
        };
        while (client.ConnectionState != ConnectionState.Connected)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(50));
        }

        await Task.Delay(Timeout.Infinite);
    }
}