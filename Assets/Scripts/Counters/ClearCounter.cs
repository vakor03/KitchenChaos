#region

using Core;
using PlayerLogic;
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
                //Clear counter doesn't have kitchen object
                if (Player.Instance!.HasKitchenObject)
                {
                    Player.Instance.KitchenObject.KitchenObjectParent = this;
                }
            }
            else
            {
                //Clear counter has kitchen object
                if (Player.Instance!.HasKitchenObject)
                {
                    if (Player.Instance!.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //Player holds plate
                        if (plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSO))
                        {
                            KitchenObject.DestroySelf();
                        }
                    }
                    else if (KitchenObject.TryGetPlate(out plateKitchenObject))
                    {
                        //Plate on the clear counter
                        if (plateKitchenObject.TryAddIngredient(Player.Instance!.KitchenObject.KitchenObjectSO))
                        {
                            Player.Instance!.KitchenObject.DestroySelf();
                        }
                    }
                }
                else
                {
                    //Picking up
                    KitchenObject.KitchenObjectParent = Player.Instance;
                }
            }
        }
    }
}