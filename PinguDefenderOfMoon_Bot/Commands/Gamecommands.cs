using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using PinguDefenderOfMoon_Bot.External_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinguDefenderOfMoon_Bot.Commands
{
    public class Gamecommands : BaseCommandModule
    {
        [Command("cg")]
        public async Task SimpleCardGame(CommandContext ctx)
        {
            var UserCard = new CardBuilder();
            var BotCard = new CardBuilder();

            var userCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Aquamarine)
                );
        }

        [Command("banorder")]
        
        public async Task BanOrderMissing(CommandContext ctx)
        {
            
        }
        

    }
}
