using DG.Tweening;
using DroneBase.Tweens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StatePanelWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _textValue;
        [SerializeField] private CountParams _params;

        private int _currentValue;

        public void SetValue(int value)
        {
            _textValue.DOCounter(
                    _currentValue,
                    value,
                    _params.Duration,
                    _params.AddThousandsSeparator)
                .SetEase(_params.Ease);

            _currentValue = value;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}