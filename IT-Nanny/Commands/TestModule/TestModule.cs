using Discord.Interactions;

namespace IT_Nanny.Commands.TestModule;

public class TestModule : InteractionModuleBase
{
    [SlashCommand("test", "test command")]
    public async Task TestAsync()
    {
        await RespondAsync("success");
    }
}