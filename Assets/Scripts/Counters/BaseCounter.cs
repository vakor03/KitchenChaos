#region

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
            set => _kitchenObject = value;
        }

        public Transform SpawnPoint => counterTopPoint;

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
            Debug.LogError("BaseCounter.InteractAlternate()");
        }
    }
}