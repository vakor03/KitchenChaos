#region

using System;
using PlayerLogic;

#endregion

namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public static event Action<TrashCounter> OnAnyTrash;

        public new static void ResetStaticData()
        {
            OnAnyTrash = null;
        }

        public override void Interact()
        {
            if (Player.Instance!.HasKitchenObject)
            {
                Player.Instance!.KitchenObject.DestroySelf();
                OnAnyTrash?.Invoke(this);
            }
        }
    }
}