using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.BotExtensions;
internal class ButtonHandler
{
    public static async Task HandleButtons(DiscordClient client, ComponentInteractionCreateEventArgs args)
    {
        if (args.Interaction.Data.CustomId.StartsWith("HelpButton"))
            await HandleHelp(args);
        if (args.Interaction.Data.CustomId.StartsWith("Test-"))
            await HandleTestButtons(args);
        if (args.Id == "dropDownList")
            await HandleDropDownList(args);
    }
    private static async Task<DiscordMessage> HandleHelp(ComponentInteractionCreateEventArgs args)
    {
        if (args.Interaction.Data.CustomId == "HelpButtonFun")
            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                             .WithContent("Fun Commands"));

        if (args.Interaction.Data.CustomId == "HelpButtonMod")
            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                            .WithContent("Moderation Commands"));

        if (args.Interaction.Data.CustomId == "HelpButtonVoice")
            await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                           .WithContent("Voice Channel Commands"));

        if (args.Interaction.Data.CustomId == "HelpButtonExit")
            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
                                                                                            .WithContent("You exited from the help list."));
        return null;
    }

    private static async Task<DiscordMessage> HandleTestButtons(ComponentInteractionCreateEventArgs args)
    {

        if (args.Interaction.Data.CustomId == "Test-1")
            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
                                                                                              .WithContent("You Pressed the 1st button"));

        else await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
                                                                                              .WithContent("You Pressed the one of the other buttons besides 1st."));
        return null;
    }

    private static async Task<DiscordMessage> HandleDropDownList(ComponentInteractionCreateEventArgs args)
    {
        string[] options = args.Values;

        foreach (string option in options)
        {
            switch (option)
            {
                case "Option-1":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
                                                                                              .WithContent("You Pressed the 1st option"));
                    break;

                case "Option-2":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
                                                                                              .WithContent("You Pressed the 2nd option"));
                    break;

                case "Option-3":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
                                                                                              .WithContent("You Pressed the 3rd option"));
                    break;
            }
        }

        return null;
    }
}
