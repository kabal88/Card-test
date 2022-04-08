using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Identifier
{
    [CreateAssetMenu]
    public class IdentifierContainer : ScriptableObject, IIdentifier
    {
        [SerializeField, ReadOnly] private int _id;
    
        public int Id
        {
            get
            {
                if (_id == 0)
                    _id = Animator.StringToHash(name); 

                return _id;
            }
        }
    }
}