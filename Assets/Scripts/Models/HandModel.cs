using System.Collections.Generic;
using Controllers;
using Tweens;
using Data;

namespace Models
{
    public class HandModel
    {
        private List<CardController> _cardsInHand = new();
        private RandomRange _randomRange;

        public IEnumerable<CardController> CardsInHand => _cardsInHand;
        public int CardsCount => _cardsInHand.Count;
        public TweenParams TweenRandomParams { get; }
        public int RandomMin => _randomRange.Min;
        public int RandomMax => _randomRange.Max;


        public HandModel(TweenParams tweenRandomParams, RandomRange range)
        {
            TweenRandomParams = tweenRandomParams;
            _randomRange = range;
        }

        public void AddCard(CardController card)
        {
            _cardsInHand.Add(card);
        }

        public void RemoveCard(CardController card)
        {
            _cardsInHand.Remove(card);
        }
        
        public void RemoveCard(int position)
        {
            _cardsInHand.Remove(_cardsInHand[position]);
        }
    }
}