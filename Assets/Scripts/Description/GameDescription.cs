using System;
using Identifier;
using Interfaces;
using Models;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class GameDescription : IGameDescription
    {
        [SerializeField] private IdentifierContainer _id;
        [SerializeField] private int _minCardsOnStart;
        [SerializeField] private int _maxCardsOnStart;
        [SerializeField] private IdentifierContainer _cardID;
        [SerializeField] private IdentifierContainer _handID;
        public int Id => _id.Id;

        public GameModel Model => new(_minCardsOnStart, _maxCardsOnStart, _cardID.Id, _handID.Id);
    }
}