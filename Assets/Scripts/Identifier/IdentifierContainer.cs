using Interfaces;
using UnityEngine;

namespace Identifier
{
    [CreateAssetMenu]
    public class IdentifierContainer : ScriptableObject, IIdentifier
    {
        [SerializeField, HideInInspector] private int ID;
    
        public int Id
        {
            get
            {
                if (ID == 0)
                    ID = Animator.StringToHash(name); 

                return ID;
            }
        }
    }
}