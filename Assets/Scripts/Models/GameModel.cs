namespace Models
{
    public class GameModel
    {
        public int MinCardsOnStart { get; private set; }
        public int MaxCardsOnStart { get; private set; }
        
        public int CardId { get; }
        
        public int HandID { get; }

        public GameModel(int minCardsOnStart, int maxCardsOnStart, int cardId, int handID)
        {
            MinCardsOnStart = minCardsOnStart;
            MaxCardsOnStart = maxCardsOnStart;
            CardId = cardId;
            HandID = handID;
        }
    }
}