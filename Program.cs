using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackCS
{
    class Program
    {
        static void Main(string[] args)
        {

            // 2. Ask the deck to make a new shuffled 52 cards
            var playAgain = "YES";
            while (playAgain.ToUpper() == "YES")
            {
                var deck = new Deck();
                deck.MakeNewShuhffledCards();

                var player = new Hand();
                var dealer = new Hand();
                // 3. Create a player hand
                for (var count = 0; count < 2; count++)
                {
                    player.AddCardToHand(deck.DealCard());

                }

                var choice = "";
                while (choice != "STAY" && !player.Busted())
                {
                    Console.WriteLine("Your hand --");
                    player.Display();
                    // 11. Ask the player if they want to HIT or STAND
                    Console.WriteLine();
                    Console.Write("HIT or STAY?");
                    choice = Console.ReadLine();
                    // 12. If HIT
                    if (choice == "HIT")
                    {
                        player.AddCardToHand(deck.DealCard());
                        //     - Ask the deck for a card and place it in the player hand, repeat step 11
                        // 13. If STAND continue on
                    }
                }

                // 16. Show the dealer's hand TotalValue
                Console.WriteLine("Your hand --");
                player.Display();

                while (!player.Busted() && dealer.TotalValue() < 17)
                {
                    dealer.AddCardToHand(deck.DealCard());
                }

                Console.WriteLine("Dealer's hand --");
                dealer.Display();

                if (player.Busted())
                {
                    Console.WriteLine("Dealer Wins! So Sorry..");
                }
                else
                {
                    if (dealer.Busted())
                    {
                        Console.WriteLine("Look at that, YOU WIN!!");
                    }
                    else
                    {
                        if (player.TotalValue() == dealer.TotalValue())
                        {
                            Console.WriteLine("It's a push so, dealer wins.. awkward..");
                        }

                    }
                }
                Console.WriteLine();
                Console.Write("Deal Again? YES or NO?");
                playAgain = Console.ReadLine();
            }
        }
    }


}

// 17. If the player busted show "DEALER WINS"
// 18. If the dealer busted show "PLAYER WINS"
// 19. If the dealer's hand is more than the player's hand then show "DEALER WINS", else show "PLAYER WINS"
// 20. If the value of the hands are even, show "DEALER WINS"




class Hand
{
    public List<Card> Cards = new List<Card>();

    public int TotalValue()
    {
        var total = 0;

        foreach (var card in Cards)
        {
            total = total + card.Value();
        }

        return total;
    }
    public bool Busted()
    {
        if (TotalValue() > 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Display()
    {
        foreach (var card in Cards)
        {
            Console.WriteLine($"The {card.Face} of {card.Suit}");
        }
        Console.WriteLine($"The total is: {TotalValue()}");
        Console.WriteLine();
    }
    public void AddCardToHand(Card cardToAdd)

    {
        Cards.Add(cardToAdd);
    }
}

class Card
{
    public string Face { get; set; }

    public string Suit { get; set; }

    public int Value()
    {
        var answer = 0;

        switch (Face)
        {
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
                answer = int.Parse(Face);
                break;

            case "Jack":
            case "King":
            case "Queen":
                answer = 10;
                break;

            case "Ace":
                answer = 11;
                break;
        }

        return answer;
    }

}
class Deck
{
    public List<Card> CardsInDeck = new List<Card>();

    public Card DealCard()
    {
        var topCard = CardsInDeck[0];
        CardsInDeck.Remove(topCard);
        return topCard;
    }

    public void MakeNewShuhffledCards()
    {
        var suits = new List<string>() { "Clubs", "Diamond", "Spade", "Heart" };

        var faces = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "King", "Queen", "Jack", "Ace" };


        foreach (var suit in suits)
        {
            foreach (var face in faces)
            {
                var ourCard = new Card()
                {
                    Face = face,
                    Suit = suit,
                };
                CardsInDeck.Add(ourCard);
            }
        }
        var n = CardsInDeck.Count();

        for (var rightIndex = n - 1; rightIndex >= 1; rightIndex--)
        {
            var randomNumberGenerator = new Random();

            var leftIndex = randomNumberGenerator.Next(rightIndex);

            var leftCard = CardsInDeck[rightIndex];

            var rightCard = CardsInDeck[leftIndex];

            CardsInDeck[rightIndex] = rightCard;

            CardsInDeck[leftIndex] = leftCard;

        }
    }
}


