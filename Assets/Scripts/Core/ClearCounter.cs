#region

using UnityEngine;

#endregion

namespace Core
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

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
    }
}