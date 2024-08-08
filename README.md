# BotTemplate

This repository represents a base template for a Discord bot using **DSharpPlus**.

## 📝 Notice

The `Config.json` file included in this repository is a placeholder. You need to copy `Config.json` into the `bin` folder and then add your token in the `Config.json` file located inside the `bin` folder.

⚠️ **Important:** Do not put your actual token in `BotConfigs/Config.json` if you plan to publish your bot publicly.

## 📂 Folder Structure

```plaintext
C:.
│   BotTemplate.sln
│
└───BotTemplate
    │   BotTemplate.csproj
    │   Program.cs
    │
    ├───bin
    │   └───Debug
    │       └───net8.0
    │               Config.json
    │               
    │
    ├───BotConfigs
    │       Config.json
    │       JsonHandler.cs
    │
    ├───BotExtensions
    │       ButtonHandler.cs
    │       WelcomeHandler.cs
    │
    ├───Commands
    │   ├───PrefixCommands
    │   │       BotSettingsCommands.cs
    │   │       TestCommands.cs
    │   │
    │   └───SlashCommands
    │           TestSlashCommanda.cs
    │
    │
    └───Utils
        ├───Configurations
        ├───Extensions
        └───Handlers
            └───ExceptionHandler
                │   GlobalExceptionHandler.cs
                │
                └───Exceptions
                        DefaultException.cs
```
## 🚀 Guideline

[`BotConfig/`](https://github.com/Novruzt/DiscordBotTemplate/tree/master/BotTemplate/BotConfigs) folder is for Bot configurations. 

[`BotExtensions/`](https://github.com/Novruzt/DiscordBotTemplate/tree/master/BotTemplate/BotExtensions) folder basically Helper for commands and events.

[`Commands/`](https://github.com/Novruzt/DiscordBotTemplate/tree/master/BotTemplate/Commands) folder is for commands both prefix and slash commands are seperated

[`Utils/`](https://github.com/Novruzt/DiscordBotTemplate/tree/master/BotTemplate/Utils/) folder are rest of project. If you have plan to implement Database or other technologies use this folder. 

***Specific guideline for classes are given inside.***


## 🔗 Additional

* [Discord API Documentation](https://discord.com/developers/docs/intro)

* [DSharp+ Repository](https://github.com/DSharpPlus/DSharpPlus)

* [Dsharp+ Documentation](https://dsharpplus.github.io/DSharpPlus/index.html)
