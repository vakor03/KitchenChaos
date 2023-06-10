#region

using UnityEngine;

#endregion

namespace Core
{
    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
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

        public abstract void Interact();
    }
}