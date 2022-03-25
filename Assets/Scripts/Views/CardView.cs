using Data;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    
    public class CardView : MonoBehaviour, ICardView
    {
        [SerializeField] private StatePanelWidget _healthPanel;
        [SerializeField] private StatePanelWidget _manaPanel;
        [SerializeField] private StatePanelWidget _attackPanel;
        [SerializeField] private Image _portrait;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;

        public void Init(CardData data)
        {
            _healthPanel.SetValue(data.Stats.Health);
            _manaPanel.SetValue(data.Stats.Mana);
            _attackPanel.SetValue(data.Stats.Attack);
            _title.text = data.Title;
            _description.text = data.Description;
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetDescription(string text)
        {
            _description.text = text;
        }

        public void SetMana(int value)
        {
            _manaPanel.SetValue(value);
        }

        public void SetHealth(int value)
        {
            _healthPanel.SetValue(value);
        }

        public void SetAttack(int value)
        {
            _attackPanel.SetValue(value);
        }

        public void SetPortrait(Sprite sprite)
        {
            _portrait.sprite = sprite;
        }
    }
}