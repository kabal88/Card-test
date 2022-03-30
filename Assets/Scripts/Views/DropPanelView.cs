using Interfaces;
using UnityEngine;

namespace View
{
    public class DropPanelView : MonoBehaviour, IDropPanel
    {
        public Transform Transform => transform;
    }
}