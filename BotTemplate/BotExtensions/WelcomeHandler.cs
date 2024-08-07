using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.BotExtensions;
internal class WelcomeHandler
{
    public static async Task<DiscordMessage> WelcomeMessage(MessageCreateEventArgs e)
    {
        if (!e.Author.IsBot && (e.Message.Content.ToLower() == "hello" || e.Message.Content.ToLower() == "hi"))
            return await e.Channel.SendMessageAsync("Hi, welcome!");
        return null;
    }
}
