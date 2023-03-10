using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinguDefenderOfMoon_Bot.Commands
{
    public class StandardCommands : BaseCommandModule
    {
        [Command("ryangosling")]
        public async Task TestCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Yo is that ryan gosling? https://variety.com/wp-content/uploads/2022/07/ryan-gosling-gray-man-premiere.jpg?w=999");
            await ctx.Channel.SendMessageAsync("no way");
            
        }

        [Command("embed")]
        public async Task SendEmbedMessage(CommandContext ctx)
        {
            var embedMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle("Seagull")
                .WithDescription("Ballin, but at what cost")
                .WithColor(DiscordColor.Lilac)
                );

            await ctx.Channel.SendMessageAsync(embedMessage);
        }
    }
}
