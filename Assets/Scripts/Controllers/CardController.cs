using Interfaces;
using Models;
using Services;
using UnityEngine;
using View;

namespace Controllers
{
    public class CardController
    {
        private CardModel _model;
        private CardView _view;

        private CardController(CardModel model, CardView view)
        {
            _model = model;
            _view = view;
        }

        public static CardController CreateCardController(ICardDescription description, Transform parent,
            Sprite portrait = default)
        {
            var model = description.Model;
            var view = GameObject.Instantiate(description.Prefab, parent).GetComponent<CardView>();

            view.Init(description.CardData);

            if (portrait == null)
            {
                ServiceLocator.Get<SpriteLoader>().LoadRandomSprite(view);
            }

            view.SetPortrait(portrait);
            return new CardController(model, view);
        }
    }
}