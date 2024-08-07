using BotTemplate.BotConfigs;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.Commands.PrefixCommands;
internal class BotSettingCommands : BaseCommandModule
{

    [Command("Prefix")]
    [Description("Changes the command prefix.")]
    public async Task ChangePrefix(CommandContext ctx, string newPrefix)
    {
        if (string.IsNullOrWhiteSpace(newPrefix))
        {
            await ctx.RespondAsync("Prefix cannot be empty.");
            return;
        }

        // Update the prefix in the JsonHandler
        JsonHandler.Prefix = newPrefix;

        // Update the config.json file
        JsonStructure config = new JsonStructure
        {
            Token = JsonHandler.Token,
            Prefix = newPrefix
        };

        await JsonHandler.WriteJsonAsync(config);

        // Respond to the user
        await ctx.RespondAsync($"Prefix successfully changed to {newPrefix}");
    }
}
