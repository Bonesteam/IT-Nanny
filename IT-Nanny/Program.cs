using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IT_Nanny.Commands.TestModule;
using Microsoft.Extensions.DependencyInjection;

namespace IT_Nanny;

public class Program
{
    private const string Token = "MTE3OTgxOTQzNDUyNTg2ODE1Mg.G5lqci.GHE1aQo5V8Xm6CPdcvbyNVXFHpQ0RMhg7V-FDo";

    public static async Task Main(string[] args)
    {
        var client = new DiscordSocketClient();
        await client.LoginAsync(TokenType.Bot, Token);
        await client.StartAsync();
        while (client.ConnectionState != ConnectionState.Connected)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(50));
        }
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IDiscordClient, DiscordSocketClient>(s => client)
            .BuildServiceProvider();
        
        var commandService = new CommandService();
        await commandService.AddModuleAsync<TestModule>(serviceProvider);
        
        await Task.Delay(Timeout.Infinite);
    }
}