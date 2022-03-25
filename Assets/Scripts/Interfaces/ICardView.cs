using UnityEngine;

namespace Interfaces
{
    public interface ICardView
    {
        void SetTitle(string title);
        void SetDescription(string text);
        void SetMana(int value);
        void SetHealth(int value);
        void SetAttack(int value);
        void SetPortrait(Sprite sprite);
    }
}