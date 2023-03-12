using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            string playersDir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "FaceitVotes", "bin", "Debug", "Players");

            var embedBuilder = new DiscordEmbedBuilder()
                .WithTitle("Votes for all players!")
                .WithColor(DiscordColor.Aquamarine);

            var totals = new Dictionary<string, int>
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

            var totalString = new StringBuilder();
            foreach (var total in sortedTotals)
            {
                totalString.AppendLine($"*{total.Key}:* {total.Value}");
            }

            embedBuilder.AddField("Total Votes", totalString.ToString());

            var embedMessage = new DiscordMessageBuilder().AddEmbed(embedBuilder);
            await ctx.Channel.SendMessageAsync(embedMessage);
        }

        [Command("displaymissing")]
        public async Task SendAllMembersVotesExcludingIds(CommandContext ctx, params int[] excludeIds)
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
                if (!excludeIds.Contains(id))
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


        [Command("edit")]
        public async Task EditMemberVotes(CommandContext ctx, string playerName, string map, int value)
        {
            string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            string playersDir = Path.Combine(path, "FaceitVotes", "bin", "Debug", "Players");

            var filePath = Path.Combine(playersDir, $"{playerName}.txt");
            if (!File.Exists(filePath))
            {
                await ctx.RespondAsync($"Player with name {playerName} does not exist.");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length != 10)
            {
                await ctx.RespondAsync($"Invalid file format for player with name {playerName}.");
                return;
            }

            var mapIndex = -1;
            switch (map.ToLower())
            {
                case "ancient":
                    mapIndex = 2;
                    break;
                case "overpass":
                    mapIndex = 3;
                    break;
                case "dust 2":
                    mapIndex = 4;
                    break;
                case "inferno":
                    mapIndex = 5;
                    break;
                case "mirage":
                    mapIndex = 6;
                    break;
                case "nuke":
                    mapIndex = 7;
                    break;
                case "vertigo":
                    mapIndex = 8;
                    break;
                case "anubis":
                    mapIndex = 9;
                    break;
                default:
                    await ctx.RespondAsync($"Invalid map: {map}");
                    return;
            }

            var votes = lines[mapIndex].Split(',');
            if (votes.Length != 1)
            {
                await ctx.RespondAsync($"Invalid votes format for player with name {playerName}.");
                return;
            }

            var oldValue = int.Parse(votes[0]);
            var newValue = value;

            if (newValue == 1 && playerName != "Linus")
            {
                await ctx.RespondAsync($"Linus, sluta försök andras värden på Inferno!");
                return;
            }

            if (newValue < 1)
            {
                newValue = 1;

            }
            if (newValue > 5)
            {
                newValue = 5;
            }

            lines[mapIndex] = newValue.ToString();

            File.WriteAllLines(filePath, lines);

            await ctx.RespondAsync($"Successfully updated {map} votes for player with name {playerName} from {oldValue} to {newValue}.");
        }

        [Command("addplayer")]
        public async Task AddPlayer(CommandContext ctx, string name, int ancientVotes = 0, int overpassVotes = 0, int dust2Votes = 0, int infernoVotes = 0, int mirageVotes = 0, int nukeVotes = 0, int vertigoVotes = 0, int anubisVotes = 0)
        {
            string playersDir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "FaceitVotes", "bin", "Debug", "Players");

            int id = Directory.GetFiles(playersDir).Length;

            string playerData = $"{id}\n{name}\n{ancientVotes}\n{overpassVotes}\n{dust2Votes}\n{infernoVotes}\n{mirageVotes}\n{nukeVotes}\n{vertigoVotes}\n{anubisVotes}";

            string filePath = Path.Combine(playersDir, $"{name}.txt");
            File.WriteAllText(filePath, playerData);

            await ctx.RespondAsync($"Added {name} ({id}) to the list of players with the following votes:\n\nAncient: {ancientVotes}\nOverpass: {overpassVotes}\nDust 2: {dust2Votes}\nInferno: {infernoVotes}\nMirage: {mirageVotes}\nNuke: {nukeVotes}\nVertigo: {vertigoVotes}\nAnubis: {anubisVotes}");
        }


        [Command("help")]

        public async Task ShowHelp(CommandContext ctx)
        {
            var embedBuilder = new DiscordEmbedBuilder()
                .WithTitle("Available Commands")
                .WithColor(DiscordColor.Aquamarine);

            embedBuilder.AddField("!ryangosling", "Do I need say more? It's ryan gosling godammit!")
                        .AddField("!displayall", "Sends a message with the current vote count for all players.")
                        .AddField("!displaymissing [player id]", "Sends a message with the current vote count for all players expect one player. Player id can be sent multiple times to exclude multiple ids")
                        .AddField("!edit [name] [map] [new vote]", "Edit the value of selected name of the selected map")
                        .AddField("!addplayer [name] [ancient] [overpass] [dust_2] [inferno] [mirage] [nuke] [vertigo] [anubis]", "Add a player to the database, ratings are between 1-5!")
                        .AddField("!help", "Displays the available commands and their descriptions.")
                        .AddField("Example", "!displaymissing 1 5 2");

            var embedMessage = new DiscordMessageBuilder().AddEmbed(embedBuilder);
            await ctx.Channel.SendMessageAsync(embedMessage);
        }
    }
}
