#region

using Core;
using PlayerLogic;

#endregion

namespace Counters
{
    public class DeliveryCounter : BaseCounter
    {
        public override void Interact()
        {
            if (Player.Instance!.HasKitchenObject)
            {
                if (Player.Instance!.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                    plateKitchenObject.DestroySelf();
                }
            }
        }
    }
}