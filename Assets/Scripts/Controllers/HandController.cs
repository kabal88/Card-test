using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers
{
    public class HandController
    {
        private HandView _view;
        private HandModel _model;
        private float duration = 1f;

        public Transform HandTransform => _view.Hand;

        private HandController(HandView view, HandModel model)
        {
            _view = view;
            _model = model;
        }

        public static HandController CreateInstance(Canvas canvas, IHandDescription description)
        {
            var view = GameObject.Instantiate(description.Prefab, canvas.transform).GetComponent<HandView>();
            var model = description.GetModel;

            return new HandController(view, model);
        }

        public void SetCardsInHand(List<CardController> cards)
        {
            for (var index = 0; index < cards.Count; index++)
            {
                var card = cards[index];
                _model.AddCard(card);
                card.SetSorting(cards.Count - index);
            }

            UpdateView();
        }

        public void AddCard(CardController card)
        {
            _model.AddCard(card);
            UpdateView();
        }

        public void RemoveCard(CardController card)
        {
            _model.RemoveCard(card);
            UpdateView();
        }


        private void UpdateView()
        {
            var cards = _model.CardsInHand.ToArray();
            var angle = _view.Angle / (_model.CardsCount - 1);
            for (var i = 0; i < cards.Length; i++)
            {
                var angleTemp = angle * i + _view.AngleOffset;
                var position = GetCardPosition(_view.Hand.position, angleTemp, _view.Radius);
                cards[i].SetPosition(position, duration);

                var rotation = GetVector3Rotation(_view.Hand.position, position);
                cards[i].SetRotation(rotation, duration);
            }
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
    }
}