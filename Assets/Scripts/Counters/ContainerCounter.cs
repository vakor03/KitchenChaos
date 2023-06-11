#region

using System;
using Core;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Counters
{
    public class ContainerCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public event Action OnItemGrabbed;

        public override void Interact()
        {
            if (!Player.Player.Instance!.HasKitchenObject)
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, Player.Player.Instance);
                
                OnItemGrabbed?.Invoke();
            }
        }
    }
}