using System;
using AssetLoaders;
using DG.Tweening;
using Enums;
using Interfaces;
using Models;
using Services;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using View;

namespace Controllers
{
    public class CardController : IDisposable
    {
        public event Action<CardController> Dead;
        public event Action<CardController> DroppedOnTable;

        private CardModel _model;
        private ICardView _view;
        private Camera _camera;

        private CardController(CardModel model, ICardView view)
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

            var controller = new CardController(model, view);

            view.HealthAnimationFinished += controller.OnHealthAnimationFinished;
            view.BeginDrag += controller.OnBeginDrag;
            view.Drag += controller.OnDrag;
            view.EndDrag += controller.OnEndDrag;

            return controller;
        }

        public void SetState(StateType type, int value)
        {
            switch (type)
            {
                case StateType.Health:
                    _view.SetHealth(value, true, _model.Health);
                    _model.SetHealth(value);
                    break;
                case StateType.Mana:
                    _view.SetMana(value, true, _model.Mana);
                    _model.SetMana(value);
                    break;
                case StateType.Attack:
                    _view.SetAttack(value, true, _model.Attack);
                    _model.SetAttack(value);
                    break;
            }
        }

        public void Remove()
        {
            _view.PlayRemoveAnimation();
        }

        public void SetPosition(Vector3 position, float duration)
        {
            _view.SetPosition(position, duration);
        }

        public void SetRotation(Quaternion rotation)
        {
            _view.SetRotation(rotation);
        }

        public void SetRotation(Vector3 rotation, float duration)
        {
            _view.SetRotation(rotation, duration);
        }

        public void SetSorting(int order)
        {
            _view.SetSorting(order);
            _model.SetSorting(order);
        }

        private void OnHealthAnimationFinished()
        {
            if (_model.Health < 1)
            {
                Dead?.Invoke(this);
            }
        }

        private void OnBeginDrag(PointerEventData eventData)
        {
            _view.SetSelection(true);
            _model.SetPreviousPosition(_view.Transform.position);
            _model.SetPreviousRotation(_view.Transform.rotation);
            _view.SetSorting(_model.DraggingSorting);
            _model.SetSorting(_model.DraggingSorting);
            _camera = Camera.main;
        }

        private void OnDrag(PointerEventData eventData)
        {
            var position = _camera.ScreenToWorldPoint(eventData.position);
            position.z = 0;
            _view.SetPosition(position);
        }

        private void OnEndDrag(PointerEventData eventData)
        {
            var ray = RectTransformUtility.ScreenPointToRay(_camera, eventData.position);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, _model.LayerMask);

            if (hit.collider != null && hit.collider.TryGetComponent(out IDropPanel panel))
            {
                _view.Transform.SetParent(panel.Transform);
                _view.SetRotation(quaternion.identity);
                DroppedOnTable?.Invoke(this);
            }
            else
            {
                _view.SetPosition(_model.PreviousPosition,0.25f);
                _view.SetRotation(_model.PreviousRotation);
            }

            _view.SetSorting(_model.PreviousSorting);
            _model.SetSorting(_model.PreviousSorting);
            _view.SetSelection(false);
        }

        public void Dispose()
        {
            _view.HealthAnimationFinished -= OnHealthAnimationFinished;
            _view.BeginDrag -= OnBeginDrag;
            _view.Drag -= OnDrag;
            _view.EndDrag -= OnEndDrag;
            _view.Dispose();
            _model = null;
        }
    }
}