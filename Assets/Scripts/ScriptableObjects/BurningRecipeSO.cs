#region

using UnityEngine;

#endregion

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int burningTimerMax;
    }
}