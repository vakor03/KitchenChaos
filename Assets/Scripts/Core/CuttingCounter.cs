#region

using System;
using UnityEngine;

#endregion

namespace Core
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private SliceRecipeSO[] sliceRecipesSO;

        private int _cuttingProgress;
        public event Action<float> OnProgressChanged;
        public event Action OnCut;

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
            }
        }

        public override void InteractAlternate()
        {
            if (HasKitchenObject && TryGetRecipeWithInput(KitchenObject.KitchenObjectSO, out SliceRecipeSO sliceRecipeSO))
            {
                _cuttingProgress++;
                OnProgressChanged?.Invoke(GetCuttingProgressNormalized(sliceRecipeSO));
                OnCut?.Invoke();
                
                if (_cuttingProgress == sliceRecipeSO.cuttingProgressMax)
                {
                    KitchenObject.DestroySelf(); 
                    KitchenObject.SpawnKitchenObject(sliceRecipeSO.output, this);
                }
            }
        }

        private bool TryGetRecipeWithInput(KitchenObjectSO inputSO, out SliceRecipeSO sliceRecipeSO)
        {
            foreach (var sliceRecipe in sliceRecipesSO)
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