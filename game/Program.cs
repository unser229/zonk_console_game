using System;
using System.Collections.Generic;

public class Card
{
    public string Suit { get; }
    public string Rank { get; }

    public Card(string suit, string rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        cards = new List<Card>();
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        Random rng = new Random();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public Card? Deal()
    {
        if (cards.Count > 0)
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
        return null; // Возвращаем null, если карт больше нет
    }
}

public class Program
{
    public static void Main()
    {
        Deck deck = new Deck();
        deck.Shuffle();

        List<Card> playerHand = new List<Card>();
        List<Card> dealerHand = new List<Card>();

        // Раздача карт игроку и дилеру
        for (int i = 0; i < 2; i++)
        {
            Card? playerCard = deck.Deal();
            Card? dealerCard = deck.Deal();

            if (playerCard != null)
            {
                playerHand.Add(playerCard);
            }
            else
            {
                Console.WriteLine("No more cards for player!");
            }

            if (dealerCard != null)
            {
                dealerHand.Add(dealerCard);
            }
            else
            {
                Console.WriteLine("No more cards for dealer!");
            }
        }

        // Вывод карт игрока
        Console.WriteLine("Player's hand:");
        foreach (Card card in playerHand)
        {
            Console.WriteLine(card);
        }

        // Вывод карт дилера
        Console.WriteLine("\nDealer's hand:");
        foreach (Card card in dealerHand)
        {
            Console.WriteLine(card);
        }
    }
}