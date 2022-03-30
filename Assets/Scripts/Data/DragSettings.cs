using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct DragSettings
    {
        public LayerMask LayerMask;
        public int DraggingSortingOrder;
    }
}