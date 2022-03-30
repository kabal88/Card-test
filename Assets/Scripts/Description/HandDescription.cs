using System;
using Data;
using Identifier;
using Interfaces;
using Models;
using Tweens;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class HandDescription : IHandDescription
    {
        [SerializeField] private IdentifierContainer _id;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private TweenParams _randomTweenParams;
        [SerializeField] private RandomRange _randomRange;

        public int Id => _id.Id;
        public GameObject Prefab => _prefab;
        public HandModel GetModel => new(_randomTweenParams, _randomRange);
    }
}