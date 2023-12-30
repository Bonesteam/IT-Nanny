using Discord.Commands;
using Discord.Interactions;

namespace IT_Nanny.Commands.TestModule;

public class TestModule : ModuleBase<SocketCommandContext>
{
    [SlashCommand("/test", "Test command")]
    public async Task TestAsync()
    {
        await ReplyAsync("success");
    }
}