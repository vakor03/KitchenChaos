using Core;
using PlayerLogic;

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
                    plateKitchenObject.DestroySelf();
                }
            }
        }
    }
}