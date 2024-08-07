using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using BotTemplate.BotConfigs;
using BotTemplate.Commands.PrefixCommands;
using BotTemplate.Commands.SlashCommands;
using BotTemplate.BotExtensions;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;

namespace BotTemplate;

internal class Program
{
    public static DiscordClient Client;
    public static CommandsNextExtension Commands;
    public static CommandsNextConfiguration CommandConfiguration;
    static async Task Main()
    {
        await JsonHandler.ReadJsonAsync();

        DiscordConfiguration discordConfiguration = new()
        {
            Intents = DiscordIntents.All,
            Token = JsonHandler.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
        };

        Client = new DiscordClient(discordConfiguration);

        Client.UseInteractivity(new InteractivityConfiguration
        {
            PollBehaviour = PollBehaviour.KeepEmojis,
            Timeout = TimeSpan.FromMinutes(1)  //Timeout for Commands Interactivity.  
        });

        //Event registrations for Client
        Client.Ready += Client_Ready;
        Client.MessageCreated += MessageCreateHandler;
        Client.ComponentInteractionCreated += ButtonPressHandler;

        CommandConfiguration = new()
        {
            StringPrefixes = new string[] { JsonHandler.Prefix },
            EnableMentionPrefix = true,
            EnableDms = true,
            EnableDefaultHelp = false,
            PrefixResolver = PrefixResolverAsync
        };

        //Event registrations for Commands
        Commands = Client.UseCommandsNext(CommandConfiguration);

        //Register commands classes to Commands.
        Commands.RegisterCommands<TestCommands>();
        Commands.RegisterCommands<BotSettingCommands>();

        //Register slash commands
        SlashCommandsExtension slashCommandsConfig = Client.UseSlashCommands();

        slashCommandsConfig.RegisterCommands<TestSlashCommands>();

        await Client.ConnectAsync();
        await Task.Delay(-1); //To keep bot running forever if project is running.
    }

    private static async Task<int> PrefixResolverAsync(DiscordMessage msg)
    {
        string currentPrefix = JsonHandler.Prefix;

        if (msg.Content.StartsWith(currentPrefix))
            // return currentPrefix.Length;
            return 1;

        return -1;
    }

    private static async Task ButtonPressHandler(DiscordClient sender, ComponentInteractionCreateEventArgs args)
    {
        await ButtonHandler.HandleButtons(sender, args); //Handling help buttons
    }

    private static async Task MessageCreateHandler(DiscordClient sender, MessageCreateEventArgs args)
    {
        await WelcomeHandler.WelcomeMessage(args); //Welcome message
    }

    private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
    {
        return Task.CompletedTask;
    }
}
