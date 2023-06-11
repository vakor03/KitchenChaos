#region

using ScriptableObjects;
using UnityEngine;

#endregion

namespace Counters
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public override void Interact()
        {
            if (!HasKitchenObject)
            {
                if (Player.Player.Instance!.HasKitchenObject)
                {
                    Player.Player.Instance.KitchenObject.KitchenObjectParent = this;
                }
            }
            else
            {
                if (!Player.Player.Instance!.HasKitchenObject)
                {
                    KitchenObject.KitchenObjectParent = Player.Player.Instance;
                }
            }
        }
    }
}