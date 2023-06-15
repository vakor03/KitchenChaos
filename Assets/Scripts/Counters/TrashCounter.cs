#region

using PlayerLogic;

#endregion

namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact()
        {
            if (Player.Instance!.HasKitchenObject)
            {
                Player.Instance!.KitchenObject.DestroySelf();   
            }
        }
    }
}