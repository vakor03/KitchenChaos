#region

using UnityEngine;

#endregion

namespace Core
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO slicedKitchenObjectSO;

        public override void Interact()
        {
            if (!HasKitchenObject)
            {
                if (Player.Instance!.HasKitchenObject)
                {
                    Player.Instance.KitchenObject.KitchenObjectParent = this;
                }
            }
            else
            {
                if (!Player.Instance!.HasKitchenObject)
                {
                    KitchenObject.KitchenObjectParent = Player.Instance;
                }
            }
        }

        public override void InteractAlternate()
        {
            if (HasKitchenObject)
            {
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(slicedKitchenObjectSO, this);
            }
        }
    }
}