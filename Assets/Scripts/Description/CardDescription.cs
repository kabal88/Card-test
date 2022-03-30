using System;
using Data;
using Identifier;
using Interfaces;
using Models;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class CardDescription : ICardDescription
    {
        [SerializeField] private IdentifierContainer _id;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Stats _stats;
        [SerializeField] private string _title;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private DragSettings _dragSettings;
        public int Id => _id.Id;
        public GameObject Prefab => _prefab;
        public CardModel Model => new(_stats, _dragSettings);
        public CardData CardData => new() {Title = _title, Description = _description, Stats = _stats};
    }
}