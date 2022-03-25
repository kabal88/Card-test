using System;
using DG.Tweening;
using UnityEngine;

namespace DroneBase.Tweens
{
    [Serializable]
    public sealed class MoveParams : TweenParams
    {
        public Vector2 Target;
        public LoopType LoopType = LoopType.Restart;
    }
}
