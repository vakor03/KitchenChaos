#region

using UnityEngine;

#endregion

namespace Core
{
    public class KitchenObject : MonoBehaviour
    {
        [field: SerializeField] public KitchenObjectSO KitchenObjectSO { get; private set; }
        private IKitchenObjectParent _kitchenObjectParent;

        public IKitchenObjectParent KitchenObjectParent
        {
            get => _kitchenObjectParent;
            set
            {
                if (_kitchenObjectParent != null)
                {
                    _kitchenObjectParent.ClearKitchenObject();
                }

                if (value.HasKitchenObject)
                {
                    Debug.LogError("IKitchenObjectParent already has KitchenObject");
                }

                _kitchenObjectParent = value;
                value.KitchenObject = this;
                
                transform.parent = value.SpawnPoint;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }

        public void DestroySelf()
        {
            _kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, 
            IKitchenObjectParent kitchenObjectParent)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().KitchenObjectParent = kitchenObjectParent;
        }
    }
} 