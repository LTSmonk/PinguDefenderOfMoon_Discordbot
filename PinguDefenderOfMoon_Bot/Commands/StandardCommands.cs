using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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

        [Command("add")]
        public async Task Addition(CommandContext ctx, int num1, int num2)
        {
            int answer = num1 + num2;
            await ctx.Channel.SendMessageAsync(answer.ToString());
        }

        [Command("johnnysins")]
        public async Task JohnnySins(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("http://www.washingtonpost.com/news/morning-mix/wp-content/uploads/sites/21/2015/06/20150427134154-astronaught.png");
        }

    }
}
