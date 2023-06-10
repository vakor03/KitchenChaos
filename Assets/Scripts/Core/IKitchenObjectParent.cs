#region

using UnityEngine;

#endregion

namespace Core
{
    public interface IKitchenObjectParent
    {
        bool HasKitchenObject { get; }
        KitchenObject KitchenObject { set; }
        Transform SpawnPoint { get; }
        void ClearKitchenObject();
    }
}