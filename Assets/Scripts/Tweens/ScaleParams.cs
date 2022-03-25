using System;

namespace DroneBase.Tweens
{
    [Serializable]
    public sealed class ScaleParams : TweenParams
    {
        public float Target;
        public float StartScale;
    }
}
