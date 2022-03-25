using Data;
using Models;
using UnityEngine;

namespace Interfaces
{
    public interface ICardDescription : IDescription
    {
        GameObject Prefab { get; }
        CardModel Model { get; }
        CardData CardData { get; }
    }
}