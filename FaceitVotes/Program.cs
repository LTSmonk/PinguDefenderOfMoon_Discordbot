using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Numerics;
using System.IO;

namespace FaceitVotes
{
    class Program
    {
        public static string baseDir = Directory.GetCurrentDirectory();
        public static readonly List<string> maps = new List<string>()
            {
                "Ancient",
                "Overpass",
                "Dust_2",
                "Inferno",
                "Mirage",
                "Nuke",
                "Vertigo",
                "Anubis"
            };

        static void Main(string[] args)
        {

            // Skapar tabellen

            MainMenu();

            // End
            Console.ReadKey();
        }

        private static void MainMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("[1] Add Player");
                    Console.WriteLine("[2] Display Result all players");

                    int menuInput = int.Parse(Console.ReadLine());

                    Console.Clear();

                    if (menuInput == 1)
                    {
                        AddPlayer();
                    }

                    if (menuInput == 2)
                    {
                        WriteVotesAll();
                    }

                    Console.ReadKey();
                }

                catch (System.FormatException)
                {
                    Console.WriteLine("Wrong input format...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }


        }


        private static void WriteVotesAll()
        {
            LoadPlayers();
        }

        public static void AddPlayer()
        {
            string playersDir = baseDir + "\\Players";
            Directory.CreateDirectory(playersDir);

            Player player = new Player();

            Console.WriteLine("Name: ");
            player.name = Console.ReadLine();


            foreach (var prop in typeof(Player).GetProperties())
            {
                if (prop.PropertyType == typeof(int))
                {
                    Console.Write($"{prop.Name}: ");
                    prop.SetValue(player, int.Parse(Console.ReadLine()));
                }
            }

            player.id = Directory.GetDirectories(playersDir).Length;

            // Write the player data to a new file using StreamWriter with FileMode.Create
            string playerDir = playersDir + "\\" + player.name + ".txt";
            using (StreamWriter writer = new StreamWriter(playerDir))
            {
                writer.WriteLine(player.id);
                writer.WriteLine(player.name);
                writer.WriteLine(player.ancient);
                writer.WriteLine(player.overpass);
                writer.WriteLine(player.dust_2);
                writer.WriteLine(player.inferno);
                writer.WriteLine(player.mirage);
                writer.WriteLine(player.nuke);
                writer.WriteLine(player.vertigo);
                writer.WriteLine(player.anubis);
            }
        }

        public static void LoadPlayers()
        {
            string playersDir = baseDir + "\\Players";
            string[] playerFiles = Directory.GetFiles(playersDir, "*.txt");

            foreach (string playerFile in playerFiles)
            {
                Player player = ReadPlayerFromFile(playerFile);
                Console.WriteLine($"Loaded player {player.name} ({player.id})");
            }
        }

        public static Player ReadPlayerFromFile(string filePath)
        {
            Player player = new Player();

            using (StreamReader reader = new StreamReader(filePath))
            {
                player.id = int.Parse(reader.ReadLine());
                player.name = reader.ReadLine();
                player.ancient = int.Parse(reader.ReadLine());
                player.overpass = int.Parse(reader.ReadLine());
                player.dust_2 = int.Parse(reader.ReadLine());
                player.inferno = int.Parse(reader.ReadLine());
                player.mirage = int.Parse(reader.ReadLine());
                player.nuke = int.Parse(reader.ReadLine());
                player.vertigo = int.Parse(reader.ReadLine());
                player.anubis = int.Parse(reader.ReadLine());
            }

            return player;
        }
    }
}