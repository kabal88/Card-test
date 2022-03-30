using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MainUI : MonoBehaviour
    {
        public event Action RandomButtonClicked;
        public event Action AddButtonClicked;
        public event Action RemoveButtonClicked;
        
        [SerializeField] private Button _randomButton;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _removeButton;

        public Transform Transform => transform;
        
        private void OnEnable()
        {
            _randomButton.onClick.AddListener(OnRandomButtonClicked);
            _addButton.onClick.AddListener(OnAddButtonClicked);
            _removeButton.onClick.AddListener(OnRemoveButtonClicked);
        }

        public void SetInteractableRandomButton(bool isOn)
        {
            _randomButton.interactable = isOn;
        }
        
        private void OnRandomButtonClicked()
        {
            RandomButtonClicked?.Invoke();
        }

        private void OnAddButtonClicked()
        {
            AddButtonClicked?.Invoke();
        }

        private void OnRemoveButtonClicked()
        {
            RemoveButtonClicked?.Invoke();
        }
    }
}