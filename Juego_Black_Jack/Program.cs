using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_Black_Jack
{
    public class Program
    {
        private static Deck deck = new Deck();
        private static Player player = new Player();


        private enum RoundResult
        {
            PUSH,
            PLAYER_WIN,
            PLAYER_BUST,
            PLAYER_BLACKJACK,
            DEALER_WIN,
            SURRENDER,
            INVALID_BET
        }

        static void InitializeHands()
        {
            deck.Initialize();

            player.Hand = deck.Deal();
            Dealer.HiddenCards = deck.Deal();
            Dealer.RevealedCards = new List<Card>();


            if (player.Hand[0].Face == Face.As && player.Hand[1].Face == Face.As)
            {
                player.Hand[1].Score = 1;
            }

            if (Dealer.HiddenCards[0].Face == Face.As && Dealer.HiddenCards[1].Face == Face.As)
            {
                Dealer.HiddenCards[1].Score = 1;
            }

            Dealer.RevealCard();

            player.AddCard();
            Dealer.AddCard();
        }


        static void StartRound()
        {
            Console.Clear();

            InitializeHands();
            TakeActions();

            Dealer.RevealCard();

            Console.Clear();
            player.AddCard();
            Dealer.AddCard();

            player.HandsCompleted++;

            if (player.Hand.Count == 0)
            {
                EndRound(RoundResult.SURRENDER);
                return;
            }
            else if (player.Generate() > 21)
            {
                EndRound(RoundResult.PLAYER_BUST);
                return;
            }

            while (Dealer.Generate() <= 16)
            {
                Dealer.RevealedCards.Add(deck.Init());

                Console.Clear();
                player.AddCard();
                Dealer.AddCard();
            }


            if (player.Generate() > Dealer.Generate())
            {
                player.Wins++;
                if (Users.IsHandBlackjack(player.Hand))
                {
                    EndRound(RoundResult.PLAYER_BLACKJACK);
                }
                else
                {
                    EndRound(RoundResult.PLAYER_WIN);
                }
            }
            else if (Dealer.Generate() > 21)
            {
                player.Wins++;
                EndRound(RoundResult.PLAYER_WIN);
            }
            else if (Dealer.Generate() > player.Generate())
            {
                EndRound(RoundResult.DEALER_WIN);
            }
            else
            {
                EndRound(RoundResult.PUSH);
            }

        }


        static void TakeActions()
        {
            string action;
            do
            {
                Console.Clear();
                player.AddCard();
                Dealer.AddCard();

                Console.Write("Enter Action (? for help): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                action = Console.ReadLine();
                Users.ResetColor();

                switch (action.ToUpper())
                {
                    case "HIT":
                        player.Hand.Add(deck.Init());
                        break;
                    case "STAND":
                        break;
                    case "SURRENDER":
                        player.Hand.Clear();
                        break;
                        player.Hand.Add(deck.Init());
                        break;
                    default:
                        Console.WriteLine("Movimientos validos: ");
                        Console.WriteLine("Hit, Stand, Surrender, Double");
                        Console.WriteLine("Presiona cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }

                if (player.Generate() > 21)
                {
                    foreach (Card card in player.Hand)
                    {
                        if (card.Score == 11) 
                        {
                            card.Score = 1;
                            break;
                        }
                    }
                }
            } while (!action.ToUpper().Equals("STAND") && !action.ToUpper().Equals("DOUBLE")
                && !action.ToUpper().Equals("SURRENDER") && player.Generate() <= 21);
        }


        static void EndRound(RoundResult result)
        {
            switch (result)
            {
            }

            if (player.Chips <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine();
                Console.WriteLine("No cuentas con fichas suficientes " + (player.HandsCompleted - 1) + " rondas.");
                Console.WriteLine("El juego ha terminado");

                player = new Player();
            }

            Users.ResetColor();
            Console.WriteLine("Presiona cualquier tecla para continuar.");
            Console.ReadKey();
            StartRound();
        }

        static void Main(string[] args)
        {
            Users.ResetColor();
            Console.Title = "♠♥♣♦ Blackjack";

            Console.WriteLine("♠♥♣♦ Bienvenido a Blackjack" + Users.GetVersionCode());
            Console.WriteLine("Presiona cualquier tecla para continuar.");
            Console.ReadKey();
            StartRound();
        }
    }
}
