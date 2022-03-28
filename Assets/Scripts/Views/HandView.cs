using Controllers;
using UnityEngine;

namespace View
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private float _radius = 3f;
        [SerializeField] private float _angle = 90f;
        [SerializeField] private int cardCount = 4;
        [SerializeField] private float _angleOffset = 90f;
        [SerializeField] private Transform _hand;

        private HandController _controller;
        public float Radius => _radius;
        public float Angle => _angle;

        public int CardCount
        {
            get => cardCount;
            set => cardCount = value;
        }

        public float AngleOffset => _angleOffset;

        public Transform Hand => _hand;
    }
}