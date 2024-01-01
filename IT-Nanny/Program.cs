using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace IT_Nanny;

public class Program
{
    private const string Token = "MTE5MDc3MzU5MDUxODQxNTQ0Mw.GN6EnV.jkrRHie6tsB360JZ9Fjtj7wmE_4MNYgoGymiYw";

    public static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<DiscordSocketClient>(s => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged,
                AlwaysDownloadUsers = true,
                LogGatewayIntentWarnings = false
            }))
            .AddSingleton<InteractionService>(s => new InteractionService(s.GetRequiredService<DiscordSocketClient>(), new InteractionServiceConfig
            {
                DefaultRunMode = RunMode.Async
            }))
            .AddSingleton<InteractionHandler>()
            .BuildServiceProvider();

        await StartAsync(serviceProvider);
    }

    private static async Task StartAsync(IServiceProvider serviceProvider)
    {
        var client = serviceProvider.GetRequiredService<DiscordSocketClient>();
        var interactionHandler = serviceProvider.GetRequiredService<InteractionHandler>();
        var interactionService = serviceProvider.GetRequiredService<InteractionService>();

        await interactionHandler.InitializeAsync();
        client.Log += async message =>
        {
            Console.WriteLine(message);
            await Task.CompletedTask;
        };
        client.Connected += async () =>
        {
            await interactionService.RegisterCommandsToGuildAsync(1178625555038482474);
        };
        await client.LoginAsync(TokenType.Bot, Token);
        await client.StartAsync();

        await Task.Delay(-1);
    }
}