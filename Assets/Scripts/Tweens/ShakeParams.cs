using System;
using UnityEngine;

namespace DroneBase.Tweens
{
    [Serializable]
    public class ShakeParams:TweenParams
    {
        public Vector2 Strength;
        public int Vibrato;
        public float Randomness;
        public bool Snapping;
        public bool FadeOut = true;
    }
}