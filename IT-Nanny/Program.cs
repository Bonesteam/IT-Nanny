using Discord;
using Discord.WebSocket;

namespace IT_Nanny;

public class Program
{
    private static DiscordSocketClient _client;

    public static async Task Main(string[] args)
    {
        _client = new DiscordSocketClient();

        _client.Log += Log;

        var token = "MTE3OTgxOTQzNDUyNTg2ODE1Mg.GDOsE-.cZvyovACd33dlTAoZKpYwffZL12jKB3pGpdpkQ";
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        _client.MessageReceived += async (m) =>
        {
            await m.Channel.SendMessageAsync("Dungeon Master");
        };
        
        await Task.Delay(-1);
    }
    
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}