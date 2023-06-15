#region

using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Core
{
    public class PlateKitchenObject : KitchenObject
    {
        [SerializeField] private List<KitchenObjectSO> validIngredientsSO;
        private List<KitchenObjectSO> _ingredients;

        private void Awake()
        {
            _ingredients = new List<KitchenObjectSO>();
        }

        public event Action<KitchenObjectSO> OnAddIngredient;

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
        {
            if (!validIngredientsSO.Contains(kitchenObjectSO) ||
                _ingredients.Contains(kitchenObjectSO))
            {
                return false;
            }
            _ingredients.Add(kitchenObjectSO);
            OnAddIngredient?.Invoke(kitchenObjectSO);
            return true;
        }
    }
}