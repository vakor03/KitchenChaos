#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class RecipeSOList : ScriptableObject
    {
        public List<RecipeSO> recipeSOList;
    }
}