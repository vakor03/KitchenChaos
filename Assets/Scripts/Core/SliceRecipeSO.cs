using UnityEngine;

namespace Core
{
    [CreateAssetMenu()]
    public class SliceRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int neededCuts;
    }
}