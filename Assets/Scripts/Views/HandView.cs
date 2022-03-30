using Controllers;
using Models;
using UnityEngine;

namespace View
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private float _radius = 3f;
        [SerializeField] private float _angleNormal = 20f;
        [SerializeField] private float _angleSmall = 10f;
        [SerializeField] private int _cardsInHandForNormalAngle = 4;
        [SerializeField] private int cardCount = 4;
        [SerializeField] private Transform _hand;

        private HandController _controller;
        private HandModel _model;
        
        public float Radius => _radius;

        public float Angle
        {
            get
            {
                if (_model == null)
                {
                    return CardCount < _cardsInHandForNormalAngle ? _angleSmall : _angleNormal;
                }
                else
                {
                    return _model.CardsCount < _cardsInHandForNormalAngle ? _angleSmall : _angleNormal;
                }
            }
        }

        public int CardCount => cardCount;

        public float AngleOffset => 90 - Angle / 2;

        public Transform Hand => _hand;

        public void Init(HandModel model)
        {
            _model = model;
        }
    }
}