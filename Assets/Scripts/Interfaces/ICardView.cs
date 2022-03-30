using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interfaces
{
    public interface ICardView : IDisposable
    {
        event Action HealthAnimationFinished;
        event Action<PointerEventData> BeginDrag;
        event Action<PointerEventData> Drag;
        event Action<PointerEventData> EndDrag;
        
        Transform Transform { get; }
        
        void SetTitle(string title);
        void SetDescription(string text);
        void SetMana(int value, bool isCount = default, int startValue = default);
        void SetHealth(int value, bool isCount = default, int startValue = default);
        void SetAttack(int value, bool isCount = default, int startValue = default);
        void SetPortrait(Sprite sprite);
        void SetPosition(Vector3 position, float duration = default);
        void SetRotation(Quaternion rotation);
        void SetRotation(Vector3 rotation, float duration = default);
        void SetSorting(int order);
        void PlayRemoveAnimation();
        void SetSelection(bool isOn);
    }
}