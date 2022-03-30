using Data;
using UnityEngine;

namespace Models
{
    public class CardModel
    {
        public int Health { get; private set; }
        public int Mana { get; private set; }
        public int Attack { get; private set; }
        
        public int CurrentSorting { get; private set; }
        
        public int PreviousSorting { get; private set; }
        
        public int DraggingSorting { get; }
        public Vector3 Position { get; private set; }
        public Vector3 PreviousPosition { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Quaternion PreviousRotation { get; private set; }

        public LayerMask LayerMask { get; }

        public CardModel(Stats stats, DragSettings dragSettings)
        {
            LayerMask = dragSettings.LayerMask;
            DraggingSorting = dragSettings.DraggingSortingOrder;
            Health = stats.Health;
            Mana = stats.Mana;
            Attack = stats.Attack;
        }

        public void SetHealth(int value)
        {
            Health = value;
        }

        public void SetMana(int value)
        {
            Mana = value;
        }

        public void SetAttack(int value)
        {
            Attack = value;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }
        
        public void SetPreviousPosition(Vector3 position)
        {
            PreviousPosition = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            Rotation = rotation;
        }

        public void SetPreviousRotation(Quaternion rotation)
        {
            PreviousRotation = rotation;
        }

        public void SetSorting(int order)
        {
            PreviousSorting = CurrentSorting;
            CurrentSorting = order;
        }
    }
}