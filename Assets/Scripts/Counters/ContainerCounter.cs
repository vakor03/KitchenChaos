#region

using System;
using Core;
using PlayerLogic;
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
            if (!Player.Instance!.HasKitchenObject)
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, Player.Instance);
                
                OnItemGrabbed?.Invoke();
            }
        }
    }
}