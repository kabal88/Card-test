using System.Collections.Generic;
using Libraries;
using Models;
using Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class GameController
    {
        private GameModel _model;
        private Library _library;
        private Canvas _canvas;
        private SpriteLoader _spriteLoader;
        private HandController _handController;

        private GameController(GameModel model, Canvas canvas, Library library)
        {
            _model = model;
            _library = library;
            _canvas = canvas;
        }

        public static GameController CreateGameController(Library library, Canvas canvas, int gameId)
        {
            var description = library.GetGameDescription(gameId);
            var model = description.Model;
            return new GameController(model, canvas, library);
        }

        public void StartGame()
        {
            ServiceLocator.SetService(InitSpriteLoader());

            _handController = HandController.CreateInstance(_canvas, _library.GetHandDescription(_model.HandID));
            CreateCards();
        }

        private SpriteLoader InitSpriteLoader()
        {
            var obj = new GameObject("SpriteLoader");
            var loader = obj.AddComponent<SpriteLoader>();
            return loader;
        }

        private void CreateCards()
        {
            var quantity = Random.Range(_model.MinCardsOnStart, _model.MaxCardsOnStart + 1);
            
            var description = _library.GetCardDescription(_model.CardId);
            

            var cards = new List<CardController>();

            for (var i = 0; i < quantity; i++)
            {
                var card = CardController.CreateCardController(description, _handController.HandTransform);
                cards.Add(card);
            }
            
            _handController.SetCardsInHand(cards);
        }
    }
}