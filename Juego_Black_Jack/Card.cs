using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Black_Jack
{
    public enum Suit
    {
        Treboles,
        Picas,
        Diamantes,
        Corazones
    }
    public enum Face
    {
        As,
        Dos,
        Tres,
        Cuatro,
        Cinco,
        Seis,
        Siete,
        Ocho,
        Nueve,
        Diez,
        Jack,
        Queen,
        King
    }
    public class Card
    {
        public Suit Suit { get; }
        public Face Face { get; }
        public int Score { get; set; }
        public char Symbol { get; }


        public Card(Suit suit, Face face)
        {
            Suit = suit;
            Face = face;

            switch (Suit)
            {
                case Treboles:
                    Symbol = '♣';
                    break;
                case Picas:
                    Symbol = '♠';
                    break;
                case Diamantes:
                    Symbol = '♦';
                    break;
                case Corazones:
                    Symbol = '♥';
                    break;
            }
            switch (Face)
            {
                case Ten:
                case Jack:
                case Queen:
                case King:
                    Score = 10;
                    break;
                case As:
                    Score = 11;
                    break;
                default:
                    Score = (int)Face + 1;
                    break;
            }
        }

        public void WriteDescription()
        {
            if (Suit == Suit.Diamantes || Suit == Suit.Corazones)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (Face == As)
            {
                if (Score == 11)
                {
                    Console.WriteLine(Symbol + " Soft " + Face + " of " + Suit);
                }
                else
                {
                    Console.WriteLine(Symbol + " Hard " + Face + " of " + Suit);
                }
            }
            else
            {
                Console.WriteLine(Symbol + " " + Face + " of " + Suit);
            }
            Users.ResetColor();
        }
    }
}
