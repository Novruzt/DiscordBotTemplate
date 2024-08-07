using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.Commands.SlashCommands;
internal class TestSlashCommands : ApplicationCommandModule
{
    [SlashCommand("Test", "testing slash command")]
    public async Task TestSlashCommand(InteractionContext context, [Option("Name", "Type name to greeting.", true)] string name = "Default")
    {
        DiscordInteractionResponseBuilder responseBuilder = new DiscordInteractionResponseBuilder()
        {
            IsEphemeral = true,
            Content = "Calling slash command",
        };

        await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, responseBuilder);

        DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder()
        {
            Title = "Test",
            Description = name
        };

        await context.Channel.SendMessageAsync(embed: embedBuilder);
    }
}
