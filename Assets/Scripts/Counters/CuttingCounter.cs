#region

using System;
using Core;
using PlayerLogic;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Counters
{
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        [SerializeField] private SliceRecipeSO[] sliceRecipeSOArray;

        private int _cuttingProgress;
        public event Action<float> OnProgressChanged;
        public event Action OnCut;
        public static event Action<CuttingCounter> OnAnyCut;

        public override void Interact()
        {
            if (!HasKitchenObject)
            {
                if (Player.Instance!.HasKitchenObject &&
                    TryGetRecipeWithInput(Player.Instance.KitchenObject.KitchenObjectSO, out SliceRecipeSO sliceRecipeSO))
                {
                    Player.Instance.KitchenObject.KitchenObjectParent = this;
                    _cuttingProgress = 0;
                    
                    OnProgressChanged?.Invoke(GetCuttingProgressNormalized(sliceRecipeSO));
                }
            }
            else
            {
                if (!Player.Instance!.HasKitchenObject)
                {
                    KitchenObject.KitchenObjectParent = Player.Instance;
                }
                else
                {
                    if (Player.Instance!.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSO))
                        {
                            KitchenObject.DestroySelf();
                        }
                    }
                }
            }
        }

        public override void InteractAlternate()
        {
            if (HasKitchenObject && TryGetRecipeWithInput(KitchenObject.KitchenObjectSO, out SliceRecipeSO sliceRecipeSO))
            {
                _cuttingProgress++;
                OnProgressChanged?.Invoke(GetCuttingProgressNormalized(sliceRecipeSO));
                OnCut?.Invoke();
                OnAnyCut?.Invoke(this);
                
                if (_cuttingProgress == sliceRecipeSO.cuttingProgressMax)
                {
                    KitchenObject.DestroySelf(); 
                    KitchenObject.SpawnKitchenObject(sliceRecipeSO.output, this);
                }
            }
        }

        private bool TryGetRecipeWithInput(KitchenObjectSO inputSO, out SliceRecipeSO sliceRecipeSO)
        {
            foreach (var sliceRecipe in sliceRecipeSOArray)
            {
                if (sliceRecipe.input == inputSO)
                {
                    sliceRecipeSO = sliceRecipe;
                    return true;
                }
            }

            sliceRecipeSO = null;
            return false;
        }

        private float GetCuttingProgressNormalized(SliceRecipeSO sliceRecipeSO)
        {
            return (float)_cuttingProgress / sliceRecipeSO.cuttingProgressMax;
        }
    }
}