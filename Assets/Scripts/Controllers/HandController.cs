using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Enums;
using Helpers;
using Interfaces;
using Models;
using UnityEngine;
using View;

namespace Controllers
{
    public class HandController : IDisposable
    {
        public event Action SequenceComplete;
        public event Action ExitSequence;

        private HandView _view;
        private HandModel _model;

        private Stack<CardController> _cardsStack = new();

        public Transform HandTransform => _view.Hand;

        private HandController(HandView view, HandModel model)
        {
            _view = view;
            _model = model;
        }

        public static HandController CreateInstance(MainUI mainUI, IHandDescription description)
        {
            var view = GameObject.Instantiate(description.Prefab, mainUI.Transform).GetComponent<HandView>();
            var model = description.GetModel;

            view.Init(model);

            var controller = new HandController(view, model);

            return controller;
        }

        public void SetCardsInHand(List<CardController> cards)
        {
            for (var index = 0; index < cards.Count; index++)
            {
                var card = cards[index];
                card.Dead += RemoveCard;
                card.DroppedOnTable += OnCardDroppedOnTable;
                _model.AddCard(card);
            }

            UpdateView();
        }

        private void OnCardDroppedOnTable(CardController card)
        {
            _model.RemoveCard(card);
            UpdateView();
        }

        public void AddCard(CardController card)
        {
            _model.AddCard(card);
            card.Dead += RemoveCard;
            card.DroppedOnTable += OnCardDroppedOnTable;
            UpdateView();
        }

        public void RemoveCard(CardController card)
        {
            _model.RemoveCard(card);
            card.Remove();
            card.Dead -= RemoveCard;
            card.DroppedOnTable -= OnCardDroppedOnTable;
            UpdateView();
        }

        public void RemoveRightCard()
        {
            var card = _model.CardsInHand.First();
            RemoveCard(card);
        }

        public void RandomSequence()
        {
            if (_model.CardsCount == 0)
            {
                ExitSequence?.Invoke();
                return;
            }

            var sequence = DOTween.Sequence();
            _cardsStack.Clear();

            foreach (var card in _model.CardsInHand)
            {
                _cardsStack.Push(card);
            }

            for (var i = 0; i < _cardsStack.Count; i++)
            {
                sequence.AppendCallback(() => ChangeRandomState(_cardsStack.Pop()))
                    .AppendInterval(_model.TweenRandomParams.Delay);
            }

            sequence.AppendCallback(OnSequenceComplete);
        }

        private void OnSequenceComplete()
        {
            SequenceComplete?.Invoke();
        }

        private void ChangeRandomState(CardController card)
        {
            var type = Randomizer.RandomEnumValue<StateType>();
            var randomValue = Randomizer.RandomInt(_model.RandomMin, _model.RandomMax);
            card.SetState(type, randomValue);
        }

        private void UpdateView()
        {
            if (_model.CardsCount == 0)
                return;

            var cards = _model.CardsInHand.ToArray();
            var angle = _view.Angle;

            if (_model.CardsCount > 1)
            {
                angle /= (_model.CardsCount - 1);

                for (var i = 0; i < cards.Length; i++)
                {
                    var card = cards[i];
                    var angleTemp = angle * i + _view.AngleOffset;
                    UpdatePositionAndRotationOfCard(card, angleTemp);
                    card.SetSorting(cards.Length - i);
                }
            }
            else
            {
                angle /= 2;
                var angleTemp = angle + _view.AngleOffset;
                UpdatePositionAndRotationOfCard(cards[0], angleTemp);
                cards[0].SetSorting(0);
            }
        }

        private void UpdatePositionAndRotationOfCard(CardController card, float angle)
        {
            var handPosition = _view.Hand.position;
            var position = GetCardPosition(handPosition, angle, _view.Radius);
            card.SetPosition(position, _model.TweenRandomParams.Duration);

            var rotation = GetVector3Rotation(handPosition, position);
            card.SetRotation(rotation, _model.TweenRandomParams.Duration);
        }

        private Vector3 GetCardPosition(Vector3 originPosition, float angle, float radius)
        {
            var pointPos = (Vector3) GetPoint(angle, radius);
            var position = originPosition + pointPos;
            return position;
        }

        private Vector2 GetPoint(float angle, float radius)
        {
            angle *= Mathf.Deg2Rad;

            var x = radius * Mathf.Cos(angle);
            var y = radius * Mathf.Sin(angle);

            var result = new Vector2(x, y);

            return result;
        }

        private Quaternion GetQuaternionRotation(Vector3 from, Vector3 to)
        {
            var direction = to - from;
            var radian = Mathf.Atan2(direction.y, direction.x);
            var angle = radian * Mathf.Rad2Deg - 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return rotation;
        }

        private Vector3 GetVector3Rotation(Vector3 from, Vector3 to)
        {
            var result = GetQuaternionRotation(from, to).eulerAngles;
            return result;
        }

        public void Dispose()
        {
            foreach (var card in _model.CardsInHand)
            {
                card.Dead -= RemoveCard;
                card.DroppedOnTable -= OnCardDroppedOnTable;
                card.Dispose();
            }
        }
    }
}