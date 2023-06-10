#region

using System;
using UnityEngine;

#endregion

namespace Core
{
    public class ContainerCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public event Action OnItemGrabbed;

        public override void Interact()
        {
            if (!Player.Instance!.HasKitchenObject)
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, Player.Instance);
                
                OnItemGrabbed?.Invoke();
            }
        }
    }
}