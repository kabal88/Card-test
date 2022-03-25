using System;
using UnityEngine;

namespace DroneBase.Tweens
{
    [Serializable]
    public sealed class FadeParams: TweenParams
    {
        [Range(0.0f, 1.0f)] public float Target;
    }
}
