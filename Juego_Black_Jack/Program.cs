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

            player.Hand = deck.DealHand();
            Dealer.HiddenCards = deck.DealHand();
            Dealer.RevealedCards = new List<Card>();

            // If hand contains two aces, make one Hard.
            if (player.Hand[0].Face == Face.Ace && player.Hand[1].Face == Face.Ace)
            {
                player.Hand[1].Value = 1;
            }

            if (Dealer.HiddenCards[0].Face == Face.Ace && Dealer.HiddenCards[1].Face == Face.Ace)
            {
                Dealer.HiddenCards[1].Value = 1;
            }

            Dealer.RevealCard();

            player.WriteHand();
            Dealer.WriteHand();
        }


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
