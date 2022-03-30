using System;
using DG.Tweening;
using Tweens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StateWidgetBase : MonoBehaviour
    {
        public event Action CountAnimationFinished;

        [SerializeField] protected CountParams _params;
        [SerializeField] protected ScaleParams _scale;

        protected IconWidget Icon;
        protected TextValueWidget TextValue;

        protected void Awake()
        {
            Icon = GetComponentInChildren<IconWidget>();
            TextValue = GetComponentInChildren<TextValueWidget>();
        }

        public virtual void SetValue(int value)
        {
            TextValue.SetText(value.ToString());
        }

        public virtual void SetValueByCount(int from, int to)
        {
            transform.localScale =
                new Vector3(_scale.StartScale, _scale.StartScale, _scale.StartScale);
            transform.DOScale(_scale.Target, _scale.Duration)
                .SetEase(_scale.Ease);
            TextValue.TextMesh.DOCounter(
                    from,
                    to,
                    _params.Duration,
                    _params.AddThousandsSeparator)
                .SetEase(_params.Ease)
                .OnComplete(OnCountAnimationFinished);
        }

        public virtual void SetIcon(Sprite icon)
        {
            Icon.SetSprite(icon);
        }

        protected virtual void OnCountAnimationFinished()
        {
            CountAnimationFinished?.Invoke();
        }
    }
}