#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public List<KitchenObjectSO> ingredients;
        public string recipeName;
    }
}