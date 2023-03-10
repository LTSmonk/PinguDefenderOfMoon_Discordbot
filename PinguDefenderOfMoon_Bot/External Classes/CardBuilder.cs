using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinguDefenderOfMoon_Bot.External_Classes
{
    internal class CardBuilder
    {
        public int[] cardValues = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public string[] cardSuits = { "Clubs", "Spades", "Diamonds", "Hearts" };

        public int SelectedNumber { get; internal set; }
        public string SelectedCard { get; internal set; }
        
        public CardBuilder() // Random Card Pull
        {
            var rnd = new Random();
            int indexNumbers = rnd.Next(0, this.cardValues.Length - 1);

            this.SelectedNumber = this.cardValues.ElementAt(indexNumbers);
            this.SelectedCard = this.cardValues.ElementAt(indexNumbers) + (" of ") + this.cardSuits.ElementAt(indexNumbers);
        }
    }
}
