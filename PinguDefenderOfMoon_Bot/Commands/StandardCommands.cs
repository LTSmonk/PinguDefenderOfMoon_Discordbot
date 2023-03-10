using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Command("displayall")]
        public async Task SendAllMembersVotes(CommandContext ctx)
        {
            string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            string playersDir = Path.Combine(path, "FaceitVotes", "bin", "Debug", "Players");

            var embedBuilder = new DiscordEmbedBuilder()
                .WithTitle("Votes for all players!")
                .WithColor(DiscordColor.Aquamarine);

            Dictionary<string, int> totals = new Dictionary<string, int>
            {
                {"Ancient", 0},
                {"Overpass", 0},
                {"Dust 2", 0},
                {"Inferno", 0},
                {"Mirage", 0},
                {"Nuke", 0},
                {"Vertigo", 0},
                {"Anubis", 0}
            };

            foreach (string filePath in Directory.GetFiles(playersDir, "*.txt"))
            {
                var playerData = File.ReadAllLines(filePath);
                embedBuilder.AddField($"{playerData[1]} [{playerData[0]}]", $"*Ancient*: {playerData[2]}\n *Overpass*: {playerData[3]}\n *Dust 2*: {playerData[4]}\n *Inferno*: {playerData[5]}\n *Mirage*: {playerData[6]}\n *Nuke*: {playerData[7]}\n *Vertigo*: {playerData[8]}\n *Anubis*: {playerData[9]}");

                totals["Ancient"] += int.Parse(playerData[2]);
                totals["Overpass"] += int.Parse(playerData[3]);
                totals["Dust 2"] += int.Parse(playerData[4]);
                totals["Inferno"] += int.Parse(playerData[5]);
                totals["Mirage"] += int.Parse(playerData[6]);
                totals["Nuke"] += int.Parse(playerData[7]);
                totals["Vertigo"] += int.Parse(playerData[8]);
                totals["Anubis"] += int.Parse(playerData[9]);
            }

            var sortedTotals = totals.OrderByDescending(x => x.Value);

            StringBuilder totalString = new StringBuilder();
            foreach (var total in sortedTotals)
            {
                totalString.AppendLine($"*{total.Key}:* {total.Value}");
            }

            embedBuilder.AddField("Total Votes", totalString.ToString());

            var embedMessage = new DiscordMessageBuilder().AddEmbed(embedBuilder);
            await ctx.Channel.SendMessageAsync(embedMessage);
        }

        [Command("displaymissing")]
        public async Task SendAllMembersVotesExcludingId(CommandContext ctx, int excludeId)
        {
            string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            string playersDir = Path.Combine(path, "FaceitVotes", "bin", "Debug", "Players");

            var embedBuilder = new DiscordEmbedBuilder()
                .WithTitle("Votes for all players!")
                .WithColor(DiscordColor.Aquamarine);

            Dictionary<string, int> totals = new Dictionary<string, int>
            {
                {"Ancient", 0},
                {"Overpass", 0},
                {"Dust 2", 0},
                {"Inferno", 0},
                {"Mirage", 0},
                {"Nuke", 0},
                {"Vertigo", 0},
                {"Anubis", 0}
            };

            foreach (string filePath in Directory.GetFiles(playersDir, "*.txt"))
            {
                var playerData = File.ReadAllLines(filePath);
                int id = int.Parse(playerData[0]);
                if (id != excludeId)
                {
                    embedBuilder.AddField($"{playerData[1]} [{playerData[0]}]", $"*Ancient*: {playerData[2]}\n *Overpass*: {playerData[3]}\n *Dust 2*: {playerData[4]}\n *Inferno*: {playerData[5]}\n *Mirage*: {playerData[6]}\n *Nuke*: {playerData[7]}\n *Vertigo*: {playerData[8]}\n *Anubis*: {playerData[9]}");
                    totals["Ancient"] += int.Parse(playerData[2]);
                    totals["Overpass"] += int.Parse(playerData[3]);
                    totals["Dust 2"] += int.Parse(playerData[4]);
                    totals["Inferno"] += int.Parse(playerData[5]);
                    totals["Mirage"] += int.Parse(playerData[6]);
                    totals["Nuke"] += int.Parse(playerData[7]);
                    totals["Vertigo"] += int.Parse(playerData[8]);
                    totals["Anubis"] += int.Parse(playerData[9]);
                }
            }



            var sortedTotals = totals.OrderByDescending(x => x.Value);

            StringBuilder totalString = new StringBuilder();
            foreach (var total in sortedTotals)
            {
                totalString.AppendLine($"*{total.Key}:* {total.Value}");
            }

            embedBuilder.AddField("Total Votes", totalString.ToString());

            var embedMessage = new DiscordMessageBuilder().AddEmbed(embedBuilder);
            await ctx.Channel.SendMessageAsync(embedMessage);
        }

        [Command("help")]

        public async Task ShowHelp(CommandContext ctx)
        {
            var embedBuilder = new DiscordEmbedBuilder()
                .WithTitle("Available Commands")
                .WithColor(DiscordColor.Aquamarine);

            embedBuilder
                       
                       .AddField("!ryangosling", "Do I need say more? It's ryan gosling godammit!")
                       .AddField("!displayall", "Sends a message with the current vote count for all players.")
                       .AddField("!displaymissing(id to be excluded)", "Sends a message with the current vote count for all players expect one player")
                       .AddField("!help", "Displays the available commands and their descriptions.");

            var embedMessage = new DiscordMessageBuilder().AddEmbed(embedBuilder);
            await ctx.Channel.SendMessageAsync(embedMessage);
        }
    }
}
