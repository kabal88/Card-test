using System;
using DG.Tweening;

namespace DroneBase.Tweens
{
    [Serializable]
    public sealed class TweenLoopParams
    {
        public int Loops;
        public LoopType Type = LoopType.Restart;
    }
}