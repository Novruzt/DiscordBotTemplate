using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.Commands.PrefixCommands;
internal class TestCommands : BaseCommandModule
{
    [Command("Test")]
    public async Task TestCommand(CommandContext context)
    {
        await context.Channel.SendMessageAsync($"Hello, {context.User.Username}. ");
    }

    [Command("Ping")]
    public async Task PingCommand(CommandContext context)
    {
        await context.Channel.SendMessageAsync($"Ping is {context.Client.Ping}ms.");
    }

    [Command("Embed")]
    [Cooldown(1, 10, CooldownBucketType.Guild)]
    public async Task EmbedMessage(CommandContext context, [RemainingText] string text)
    {
        DiscordEmbedBuilder builder = new DiscordEmbedBuilder()
        {
            Color = DiscordColor.Green,
            Title = "Embed message was called.",
            Description = text + $"by {context.User.Mention}" ?? $"Called by {context.User.Mention}"
        };

        await context.Channel.SendMessageAsync(embed: builder);
    }

    [Command("Help")]
    [Cooldown(1, 10, CooldownBucketType.Guild)]
    public async Task HelpCommand(CommandContext context)
    {
        DiscordButtonComponent funButton = new DiscordButtonComponent(ButtonStyle.Primary, "HelpButtonFun", "Fun");
        DiscordButtonComponent modButton = new DiscordButtonComponent(ButtonStyle.Primary, "HelpButtonMod", "Moderation");
        DiscordButtonComponent voiceButton = new DiscordButtonComponent(ButtonStyle.Primary, "HelpButtonVoice", "Voice");
        DiscordButtonComponent exitButton = new DiscordButtonComponent(ButtonStyle.Danger, "HelpButtonExit", "Exit");

        DiscordMessageBuilder helpMessage = new DiscordMessageBuilder()
                                             .AddEmbed(new DiscordEmbedBuilder()
                                             .WithColor(DiscordColor.White)
                                             .WithTitle("Help Menu")
                                             .WithDescription("Please click button for more information."))
                                             .AddComponents(funButton, modButton, voiceButton, exitButton);

        await context.Channel.SendMessageAsync(helpMessage);
    }

    [Command("Button")]
    [Cooldown(1, 10, CooldownBucketType.Guild)]
    public async Task ButtonTest(CommandContext context)
    {
        DiscordButtonComponent buttonComponent1 = new DiscordButtonComponent(ButtonStyle.Success, "Test-1", "Button 1");
        DiscordButtonComponent buttonComponent2 = new DiscordButtonComponent(ButtonStyle.Danger, "Test-2", "Button 2");
        DiscordButtonComponent buttonComponent3 = new DiscordButtonComponent(ButtonStyle.Primary, "Test-3", "Button 3");
        DiscordButtonComponent buttonComponent4 = new DiscordButtonComponent(ButtonStyle.Secondary, "Test-4", "Button 4");

        DiscordMessageBuilder messageBuilder = new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder()
                                                    .WithTitle("Message with button")
                                                    .WithColor(DiscordColor.White)
                                                    .WithDescription("Click button to continue."))
                                                    .AddComponents(buttonComponent1, buttonComponent2, buttonComponent3, buttonComponent4);

        await context.Channel.SendMessageAsync(messageBuilder);
    }

    [Command("Dropdown")]
    [Cooldown(1, 10, CooldownBucketType.Guild)]
    public async Task DropDownList(CommandContext context)
    {
        List<DiscordSelectComponentOption> options = new List<DiscordSelectComponentOption>
            {
                new DiscordSelectComponentOption("Option 1", "Option-1"),
                new DiscordSelectComponentOption("Option 2", "Option-2"),
                new DiscordSelectComponentOption("Option 3", "Option-3")
            };

        DiscordSelectComponent dropDown = new DiscordSelectComponent("dropDownList", "Select...", options);

        DiscordMessageBuilder dropDownMessage = new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder()
                                                                                      .WithColor(DiscordColor.Gold)
                                                                                      .WithTitle("This embed has a DropDownList"))
                                                                                      .AddComponents(dropDown);

        await context.Channel.SendMessageAsync(dropDownMessage);
    }
}
