using BotTemplate.Utils.Handlers.ExceptionHandler.Exceptions;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands.Attributes;
using DSharpPlus.SlashCommands.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.Utils.Handlers.ExceptionHandler;
internal class GlobalExceptionHandler
{

    #region PrefixCommands
    public static async Task HandleCommandErrors(CommandErrorEventArgs args)
    {
        if (args != null && args.Exception != null)
        {
            if (args.Exception is ChecksFailedException exception)
            {
                foreach (CheckBaseAttribute check in exception.FailedChecks)
                {
                    switch (check)
                    {
                        case CooldownAttribute cooldownCheck:
                            await HandleCooldownAsync(args, cooldownCheck);
                            break;

                        case RequirePermissionsAttribute requirePermissionsCheck:
                            await HandlePermissionAsync(args, requirePermissionsCheck);
                            break;

                        // Add more cases for other attribute types as needed

                        default:
                            await HandleDefaultAsync(args);
                            break;
                    }
                }
            }


            // Add custom Exceptions above DefaultException and make sure they are derivived from DefaultException
            if (args.Exception is DefaultException defaultException)
                await HandleDefaultAsync(args);
        }
    }

    private static async Task<DiscordMessage> HandleCooldownAsync(CommandErrorEventArgs args, CooldownAttribute cooldown)
    {
        TimeSpan remainingCooldown = cooldown.GetRemainingCooldown(args.Context);
        string formattedTime = $"{(int)remainingCooldown.TotalHours}h{(int)remainingCooldown.Minutes}m{remainingCooldown.Seconds}s";

        DiscordEmbedBuilder cooldownMessage = new DiscordEmbedBuilder()
        {
            Color = DiscordColor.Red,
            Title = "Please, wait for the cooldown to end.",
            Description = formattedTime
        };

        return await args.Context.Channel.SendMessageAsync(embed: cooldownMessage);
    }

    private static async Task<DiscordMessage> HandlePermissionAsync(CommandErrorEventArgs args, RequirePermissionsAttribute permission)
    {
        DiscordMessageBuilder messageBuilder = new DiscordMessageBuilder()
        {
            Content = "You don't have permission to do this.\n" +
                      "Excepted permission:" + permission.Permissions.ToString()
        };

        return await args.Context.Channel.SendMessageAsync(messageBuilder);
    }

    private static async Task<DiscordMessage> HandleDefaultAsync(CommandErrorEventArgs args)
    {
        DiscordMessageBuilder messageBuilder = new DiscordMessageBuilder()
        {
            Content = "Something went wrong. See log for further information."
        };

        return await args.Context.Channel.SendMessageAsync(messageBuilder);
    }

    #endregion

    #region SlashCommands

    public static async Task HandleSlashCommandErrors(SlashCommandErrorEventArgs args)
    {
        if (args != null && args.Exception != null)
        {
            if (args.Exception is SlashExecutionChecksFailedException exception)
            {
                foreach (SlashCheckBaseAttribute check in exception.FailedChecks)
                {
                    switch (check)
                    {

                        case SlashRequirePermissionsAttribute permissionsAttribute:
                            await HandlePermissionAsync(args, permissionsAttribute);
                            break;


                        // Add more cases for other attribute types as needed

                        default:
                            await HandleDefaultAsync(args);
                            break;
                    }
                }
            }

            // Add custom Exceptions above DefaultException and make sure they are derivived from DefaultException
            if (args.Exception is DefaultException defaultException)
                await HandleDefaultAsync(args);

            DiscordChannel channel = args.Context.Guild.GetChannel(784733575936737304);
            DiscordEmbedBuilder logBuilder = new DiscordEmbedBuilder()
            {
                Title = "Failed log message.",
                Description = "Failed command: " + args.Context.CommandName + "\n"
                + "User: " + args.Context.User.Username + "\n" + "UserId: " + args.Context.User.Id + "\n\n\n\n\n"
                + "Exception: " + args.Exception.Message,
                Color = DiscordColor.Red
            };

            await channel.SendMessageAsync(embed: logBuilder);
        }
    }

    private static async Task HandlePermissionAsync(SlashCommandErrorEventArgs args, SlashRequirePermissionsAttribute permission)
    {
        DiscordInteractionResponseBuilder responseBuilder = new DiscordInteractionResponseBuilder()
        {
            IsEphemeral = true,
            Content = "Permission excepted: " + permission.Permissions.ToString(),
        };

        await args.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, responseBuilder);
    }

    private static async Task HandleDefaultAsync(SlashCommandErrorEventArgs args)
    {
        DiscordWebhookBuilder webhookBuilder = new DiscordWebhookBuilder()
        {
            Content = "Something went wrong."
        };

        await args.Context.EditResponseAsync(webhookBuilder);
    }

    #endregion
}
