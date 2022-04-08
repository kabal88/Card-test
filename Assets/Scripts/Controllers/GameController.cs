using System;
using System.Collections.Generic;
using AssetLoaders;
using Helpers;
using Interfaces;
using Libraries;
using Models;
using Services;
using UnityEngine;
using View;

namespace Controllers
{
    public class GameController : IDisposable
    {
        private GameModel _model;
        private Library _library;
        private MainUI _mainUI;
        private HandController _handController;

        private GameController(GameModel model, MainUI mainUI, Library library)
        {
            _model = model;
            _library = library;
            _mainUI = mainUI;
        }

        public static GameController CreateInstance(Library library, MainUI ui, int gameId)
        {
            var description = library.GetGameDescription(gameId);
            var model = description.Model;
            return new GameController(model, ui, library);
        }
        
        public void StartGame()
        {
            ServiceLocator.SetService(CreateSpriteLoader());

            _handController = HandController.CreateInstance(_mainUI, _library.GetHandDescription(_model.HandID));
            CreateCards();
            _handController.SequenceComplete += OnSequenceComplete;
            _handController.ExitSequence += OnExitSequence;

            _mainUI.AddButtonClicked += OnAddCardClicked;
            _mainUI.RemoveButtonClicked += OnRemoveCardClicked;
            _mainUI.RandomButtonClicked += OnRandomClicked;
        }

        private void OnExitSequence()
        {
            _mainUI.SetInteractableRandomButton(true);
        }

        private void OnSequenceComplete()
        {
            _handController.RandomSequence();
        }

        private void OnAddCardClicked()
        {
            var description = _library.GetCardDescription(_model.CardId);
            var card = CreateCard(description);
            _handController.AddCard(card);
        }

        private void OnRemoveCardClicked()
        {
            _handController.RemoveRightCard();
        }

        private void OnRandomClicked()
        {
            _mainUI.SetInteractableRandomButton(false);
            _handController.RandomSequence();
        }

        private SpriteLoader CreateSpriteLoader()
        {
            var obj = new GameObject("SpriteLoader");
            var loader = obj.AddComponent<SpriteLoader>();
            return loader;
        }

        private CardController CreateCard(ICardDescription description)
        {
            return CardController.CreateCardController(description, _handController.HandTransform);
        }

        private void CreateCards()
        {
            var quantity = Randomizer.RandomInt(_model.MinCardsOnStart, _model.MaxCardsOnStart);

            var description = _library.GetCardDescription(_model.CardId);


            var cards = new List<CardController>();

            for (var i = 0; i < quantity; i++)
            {
                var card = CreateCard(description);
                cards.Add(card);
            }

            _handController.SetCardsInHand(cards);
        }

        public void Dispose()
        {
            _handController.SequenceComplete -= OnSequenceComplete;
            _handController.ExitSequence -= OnExitSequence;
            _handController?.Dispose();
            
            _mainUI.AddButtonClicked -= OnAddCardClicked;
            _mainUI.RemoveButtonClicked -= OnRemoveCardClicked;
            _mainUI.RandomButtonClicked -= OnRandomClicked;
        }
    }
}