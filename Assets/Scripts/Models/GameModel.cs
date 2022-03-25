namespace Models
{
    public class GameModel
    {
        public int MinCardsOnStart { get; private set; }
        public int MaxCardsOnStart { get; private set; }
        
        public int CardId { get; private set; }

        public GameModel(int minCardsOnStart, int maxCardsOnStart, int cardId)
        {
            MinCardsOnStart = minCardsOnStart;
            MaxCardsOnStart = maxCardsOnStart;
            CardId = cardId;
        }
    }
}