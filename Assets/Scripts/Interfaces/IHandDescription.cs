using Models;
using UnityEngine;

namespace Interfaces
{
    public interface IHandDescription : IDescription
    {
        GameObject Prefab { get; }
        HandModel GetModel { get; }
    }
}