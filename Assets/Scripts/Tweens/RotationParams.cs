using System;
using UnityEngine;

namespace DroneBase.Tweens
{
    [Serializable]
    public sealed class RotationParams : TweenParams
    {
        public Vector3 Target;
        public Vector3 StartRoration;
    }
}