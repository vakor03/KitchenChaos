namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact()
        {
            if (Player.Player.Instance!.HasKitchenObject)
            {
                Player.Player.Instance!.KitchenObject.DestroySelf();   
            }
        }
    }
}