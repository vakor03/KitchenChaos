#region

using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace Core
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private SliceRecipeSO[] sliceRecipesSO;

        private int _cuttingProgress;

        public override void Interact()
        {
            if (!HasKitchenObject)
            {
                if (Player.Instance!.HasKitchenObject &&
                    TryGetRecipeWithInput(Player.Instance.KitchenObject.KitchenObjectSO, out _))
                {
                    Player.Instance.KitchenObject.KitchenObjectParent = this;
                    _cuttingProgress = 0;
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
                
                if (_cuttingProgress == sliceRecipeSO.neededCuts)
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
    }
}