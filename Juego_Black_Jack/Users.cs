using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Black_Jack
{
    public class Users
    {
        private static string versionCode = "1.0";

        public static int MinimumBet { get; } = 10;
        public static string GetVersionCode() { return versionCode; }

        public static bool IsHandBlackjack(List<Card> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].Face == Face.As && hand[1].Score == 10) return true;
                else if (hand[1].Face == Face.As && hand[0].Score == 10) return true;
            }
            return false;
        }


        public static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    public class Player
    {
        public int Chips { get; set; } = 500;
        public int Bet { get; set; }
        public int Wins { get; set; }
        public int HandsCompleted { get; set; } = 1;

        public List<Card> Hand { get; set; }

        public int Generate()
        {
            int value = 0;
            foreach (Card card in Hand)
            {
                value += card.Score;
            }
            return value;
        }

        public void AddCard()
        {
            Users.ResetColor();
            Console.Write("Chips: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Chips + "  ");
            Users.ResetColor();
            Console.Write("Wins: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Wins);
            Users.ResetColor();
            Console.WriteLine("Round #" + HandsCompleted);

            Console.WriteLine();
            Console.WriteLine("Your Hand (" + Generate() + "):");
            foreach (Card card in Hand)
            {
                card.WriteDescription();
            }
            Console.WriteLine();
        }
    }

    public class Dealer
    {
        public static List<Card> HiddenCards { get; set; } = new List<Card>();
        public static List<Card> RevealedCards { get; set; } = new List<Card>();

        public static void RevealCard()
        {
            RevealedCards.Add(HiddenCards[0]);
            HiddenCards.RemoveAt(0);
        }


        public static int Generate()
        {
            int value = 0;
            foreach (Card card in RevealedCards)
            {
                value += card.Score;
            }
            return value;
        }


        public static void AddCard()
        {
            Console.WriteLine("Dealer's Hand (" + Generate() + "):");
            foreach (Card card in RevealedCards)
            {
                card.WriteDescription();
            }
            for (int i = 0; i < HiddenCards.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("<hidden>");
                Users.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
