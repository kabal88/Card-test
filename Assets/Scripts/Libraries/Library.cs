using System;
using System.Collections.Generic;
using System.Linq;
using Descriptions;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Libraries
{
    [Serializable]
    public sealed class Library
    {
        [SerializeField] private List<Description> Descriptions = new();

        private Dictionary<int, ICardDescription> _cardDescriptions = new();
        private Dictionary<int, IGameDescription> _gameDescriptions = new();
        private Dictionary<int, IHandDescription> _handDescriptions = new();

        public void Init()
        {
            foreach (var description in Descriptions)
            {
                switch (description.GetDescription)
                {
                    case ICardDescription data:
                        _cardDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case IGameDescription data:
                        _gameDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case IHandDescription data:
                        _handDescriptions.Add(description.GetDescription.Id,data);
                        break;
                }
            }
        }

        /// <summary>
        /// Work ONLY from Editor. Use after adding new Description to project. 
        /// </summary>
        [Button(ButtonSizes.Large), GUIColor(0.5f, 0.8f, 1f), PropertyTooltip("Click after adding new Description to project.")]
        public void CollectAllDescriptions()
        {
            Descriptions = AssetDatabase.FindAssets("t:Description")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Description>).ToList();
        }

        public ICardDescription GetCardDescription(int id)
        {
            if (_cardDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"Card description with id {id} not found");
        }
        
        public IGameDescription GetGameDescription(int id)
        {
            if (_gameDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"Game description with id {id} not found");
        }
        
        public IHandDescription GetHandDescription(int id)
        {
            if (_handDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"Hand description with id {id} not found");
        }
        
    }
}