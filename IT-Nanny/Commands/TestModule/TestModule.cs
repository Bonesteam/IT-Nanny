using Discord.Interactions;
using JetBrains.Annotations;

namespace IT_Nanny.Commands.TestModule;

[UsedImplicitly]
public class TestModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("test", "test command")]
    public async Task TestAsync()
    {
        await RespondAsync("success");
    }
    
    [SlashCommand("myuser", "get my id")]
    public async Task MyUser()
    {
        await RespondAsync($"Your user id is: {Context.User.Id}");
    }
}