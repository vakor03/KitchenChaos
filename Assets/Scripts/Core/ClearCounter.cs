#region

using UnityEngine;

#endregion

namespace Core
{
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform counterTopPoint;

        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        private KitchenObject _kitchenObject;

        public bool HasKitchenObject => _kitchenObject != null;

        public KitchenObject KitchenObject
        {
            set => _kitchenObject = value;
        }

        public Transform SpawnPoint => counterTopPoint;

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }


        public void Interact()
        {
            if (_kitchenObject is null)
            {
                Transform kitchenObject = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
                kitchenObject.GetComponent<KitchenObject>().KitchenObjectParent = this;
            }
            else
            {
                _kitchenObject.KitchenObjectParent = Player.Instance;
            }
        }
    }
}
