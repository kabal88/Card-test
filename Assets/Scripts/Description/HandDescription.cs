using System;
using Identifier;
using Interfaces;
using Models;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class HandDescription : IHandDescription
    {
        [SerializeField] private IdentifierContainer _id;
        [SerializeField] private GameObject _prefab;
        public int Id => _id.Id;
        public GameObject Prefab => _prefab;
        public HandModel GetModel => new();

    }
}