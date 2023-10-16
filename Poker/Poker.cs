namespace Poker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                User user1 = new User();
                User user2 = new User();

                Console.WriteLine(user1.ToString() + " " + user2.ToString());
                Console.WriteLine(user1.GetTier().ToString() + " " + user2.GetTier().ToString());

                if (user1.GetTier() == Tier.RoyalStraightFlush || user2.GetTier() == Tier.RoyalStraightFlush)
                {
                    break;
                }

                if (user1.GetTier() > user2.GetTier())
                {
                    Console.WriteLine("User 1 win");
                }
                else if (user1.GetTier() < user2.GetTier())
                {
                    Console.WriteLine("User 2 win");
                }
                else
                {

                }

                Card.UsedCards.Clear();
            }
        }
    }

    enum Type { C, S, D, H }

    class Card
    {
        private int number;
        public int Number
        {
            get { return this.number; }
            set
            {
                if (this.number < 0 || this.number > 14)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.number = value;
            }
        }

        public Type Type { get; set; }

        public static List<Card> UsedCards = new List<Card>();

        public static Card RandomCard()
        {
            Card card = new Card();

            do
            {
                card.Number = new Random(DateTime.Now.Microsecond).Next(1, 14);
                card.Type = (Type)new Random(DateTime.Now.Microsecond).Next(0, 4);

            } while (Card.UsedCards.Find(x => x.Number == card.Number && x.Type == card.Type) != null);

            Card.UsedCards.Add(card);

            return card;
        }
    }

    enum Tier { HighCard, OnePair, TwoPair, Triple, Straight, Flush, FullHouse, Quads, StraightFlush, RoyalStraightFlush }

    class User
    {
        public List<Card> Cards { get; set;}

        public User()
        {
            this.Cards = new List<Card>(new Card[5]);

            for (int i = 0; i < 5; i++)
            {
                this.Cards[i] = Card.RandomCard();
            }
        }

        public Tier GetTier()
        {
            bool flush = IsFlush();
            bool straight = IsStraight();

            if (flush == true && straight == true)
            {
                if (this.Cards.Find(x => x.Number == 14) != null)
                {
                    return Tier.RoyalStraightFlush;
                }
                else
                {
                    return Tier.StraightFlush;
                }
            }
            else if (flush == true)
            {
                return Tier.Flush;
            }
            else if (straight == true)
            {
                return Tier.Straight;
            }
            else
            {
                return Tier.HighCard; // 나머지 경우를 상정
            }
        }

        private bool IsFlush()
        {
            for (int i = 1; i < this.Cards.Count; i++)
            {
                if (this.Cards[0].Type != this.Cards[i].Type)
                {
                    return false;
                }
            }

            return true;
        }
        private bool IsStraight()
        {
            Card[] cards = this.Cards.ToArray();
            Array.Sort(cards, (x, y) =>
            {
                if (x.Number < y.Number) return -1;
                else if (x.Number > y.Number) return 1;
                else return 0;
            });

            if (cards[0].Number == 1 && cards[1].Number == 10)
            {
                this.Cards.Find(x => x.Number == 1)!.Number = 14;

                return true;
            }

            for (int i = cards[0].Number; i - cards[0].Number < cards.Length; i++)
            {
                if (i != cards[i - cards[0].Number].Number)
                {
                    return false;
                }
            }

            return true;
        }

        public new string ToString()
        {
            string result = string.Empty;

            for (int i = 0; i < this.Cards.Count; i++)
            {
                result += this.Cards[i].Type.ToString() + this.Cards[i].Number.ToString() + " ";
            }

            return result;
        }
    }
}
