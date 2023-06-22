#region

using System;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace Counters
{
    public class DeliveryManager : MonoBehaviour
    {
        [SerializeField] private RecipeSOList recipeSOList;
        private float _spawnRecipeTimer;
        private float _spawnRecipeTimerMax = 4f;
        private int _waitingRecipesMax = 4;

        private List<RecipeSO> _waitingRecipeSOList;

        public List<RecipeSO> WaitingRecipeSOList => _waitingRecipeSOList;

        public static DeliveryManager Instance { get; private set; }
        public int RecipesDelivered { get; private set; }

        private void Awake()
        {
            Instance = this;
            _waitingRecipeSOList = new List<RecipeSO>();
        }

        private void Update()
        {
            _spawnRecipeTimer -= Time.deltaTime;
            if (_spawnRecipeTimer <= 0f)
            {
                _spawnRecipeTimer = _spawnRecipeTimerMax;
                if (_waitingRecipeSOList.Count < _waitingRecipesMax)
                {
                    RecipeSO waitingRecipeSO =
                        recipeSOList.recipeSOList[Random.Range(0, recipeSOList.recipeSOList.Count)];
                    _waitingRecipeSOList.Add(waitingRecipeSO);
                    OnRecipeSpawned?.Invoke();
                }
            }
        }

        public event Action OnRecipeSpawned;
        public event Action OnRecipeCompleted;
        public event Action OnRecipeSuccess;
        public event Action OnRecipeFailed;

        public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
        {
            for (var i = 0; i < _waitingRecipeSOList.Count; i++)
            {
                RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

                if (waitingRecipeSO.ingredients.Count != plateKitchenObject.KitchenObjectSOList.Count)
                {
                    continue;
                }

                bool plateContentsMatches = CompareRecipeWithPlateContent(plateKitchenObject, waitingRecipeSO);

                if (plateContentsMatches)
                {
                    _waitingRecipeSOList.RemoveAt(i);
                    RecipesDelivered++;
                    OnRecipeCompleted?.Invoke();
                    OnRecipeSuccess?.Invoke();
                    return;
                }
            }
            OnRecipeFailed?.Invoke();
        }

        private static bool CompareRecipeWithPlateContent(PlateKitchenObject plateKitchenObject, RecipeSO waitingRecipeSO)
        {
            bool plateContentsMatches = true;
            foreach (var ingredientSO in waitingRecipeSO.ingredients)
            {
                bool ingredientFound = false;
                foreach (var plateKitchenObjectSO in plateKitchenObject.KitchenObjectSOList)
                {
                    if (ingredientSO == plateKitchenObjectSO)
                    {
                        ingredientFound = true;
                        break;
                    }
                }

                if (!ingredientFound)
                {
                    plateContentsMatches = false;
                }
            }

            return plateContentsMatches;
        }
    }
}