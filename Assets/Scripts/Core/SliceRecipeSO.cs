#region

using UnityEngine;

#endregion

namespace Core
{
    [CreateAssetMenu()]
    public class SliceRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int cuttingProgressMax;
    }
}