#region

using System;
using Core;
using UnityEngine;

#endregion

namespace Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform counterTopPoint;

        private KitchenObject _kitchenObject;

        public bool HasKitchenObject => _kitchenObject != null;

        public KitchenObject KitchenObject
        {
            get => _kitchenObject;
            set
            {
                _kitchenObject = value;
                if (_kitchenObject!=null)
                {
                    OnAnyObjectPlacedHere?.Invoke(this);
                }
            }
        }

        public Transform SpawnPoint => counterTopPoint;
        public static event Action<BaseCounter> OnAnyObjectPlacedHere;

        public static void ResetStaticData()
        {
            OnAnyObjectPlacedHere = null;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public virtual void Interact()
        {
            Debug.LogError("BaseCounter.Interact()");
        }

        public virtual void InteractAlternate()
        {
            // Debug.LogError("BaseCounter.InteractAlternate()");
        }
    }
}