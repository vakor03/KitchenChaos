using UnityEngine;

namespace Core
{
    [CreateAssetMenu()]
    public class SliceRecipe : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
    }
}