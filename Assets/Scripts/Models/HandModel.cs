using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models
{
    public class HandModel
    {
        private List<CardController> _cardsInHand = new();

        public IEnumerable<CardController> CardsInHand => _cardsInHand;
        public int CardsCount => _cardsInHand.Count;

        public void AddCard(CardController card)
        {
            _cardsInHand.Add(card);
        }

        public void RemoveCard(CardController card)
        {
            _cardsInHand.Remove(card);
        }
    }
}