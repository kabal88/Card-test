using System;
using Data;
using DG.Tweening;
using Tweens;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class CardView : MonoBehaviour, ICardView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action HealthAnimationFinished;
        public event Action<PointerEventData> BeginDrag;
        public event Action<PointerEventData> Drag;
        public event Action<PointerEventData> EndDrag;

        [SerializeField] private ScaleParams _scaleParams;

        private PortraitWidget _portrait;
        private TitleWidget _title;
        private DescriptionWidget _description;
        private SortingComponent _sorting;
        private GlowWidget _glowWidget;
        private HealthStateWidget _health;
        private ManaStateWidget _mana;
        private AttackStateWidget _attack;

        public Transform Transform => transform;

        private void Awake()
        {
            _portrait = GetComponentInChildren<PortraitWidget>(true);
            _title = GetComponentInChildren<TitleWidget>(true);
            _description = GetComponentInChildren<DescriptionWidget>(true);
            _sorting = GetComponentInChildren<SortingComponent>(true);
            _glowWidget = GetComponentInChildren<GlowWidget>(true);
            _health = GetComponentInChildren<HealthStateWidget>(true);
            _mana = GetComponentInChildren<ManaStateWidget>(true);
            _attack = GetComponentInChildren<AttackStateWidget>(true);
        }

        private void OnEnable()
        {
            _health.CountAnimationFinished += OnHealthCountAnimationFinished;
        }

        private void OnDisable()
        {
            _health.CountAnimationFinished -= OnHealthCountAnimationFinished;
        }

        public void Init(CardData data)
        {
            _health.SetValue(data.Stats.Health);
            _mana.SetValue(data.Stats.Mana);
            _attack.SetValue(data.Stats.Attack);
            _title.SetText(data.Title);
            _description.SetText(data.Description);
        }

        public void SetTitle(string title)
        {
            _title.SetText(title);
        }

        public void SetDescription(string text)
        {
            _description.SetText(text);
        }

        public void SetMana(int value, bool isCount = false, int startValue = default)
        {
            if (isCount)
            {
                _mana.SetValueByCount(startValue, value);
            }
            else
            {
                _mana.SetValue(value);
            }
        }

        public void SetHealth(int value, bool isCount = false, int startValue = default)
        {
            if (isCount)
            {
                _health.SetValueByCount(startValue, value);
            }
            else
            {
                _health.SetValue(value);
            }
        }

        public void SetAttack(int value, bool isCount = false, int startValue = default)
        {
            if (isCount)
            {
                _attack.SetValueByCount(startValue, value);
            }
            else
            {
                _attack.SetValue(value);
            }
        }

        public void SetPortrait(Sprite sprite)
        {
            _portrait.SetSprite(sprite);
        }

        public void SetPosition(Vector3 position, float duration = default)
        {
            if (duration == 0)
            {
                transform.position = position;
            }
            else
            {
                Transform.DOMove(position, duration);
            }

        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        
        public void SetRotation(Vector3 rotation, float duration = default)
        {
            Transform.DORotate(rotation, duration);
        }

        public void SetSorting(int order)
        {
            _sorting.SetSorting(order);
        }

        private void OnHealthCountAnimationFinished()
        {
            HealthAnimationFinished?.Invoke();
        }

        public void PlayRemoveAnimation()
        {
            gameObject.SetActive(false);
        }

        public void SetSelection(bool isOn)
        {
            _glowWidget.Glow(isOn);

            if (isOn)
            {
                Transform.DOScale(_scaleParams.Target, _scaleParams.Duration).SetEase(_scaleParams.Ease);
            }
            else
            {
                Transform.DOScale(_scaleParams.StartScale, _scaleParams.Duration).SetEase(_scaleParams.Ease);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag?.Invoke(eventData);
        }

        public void Dispose()
        {
            Destroy(this);
        }
    }
}