#region

using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace Core
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private SliceRecipe[] sliceRecipesSO;

        public override void Interact()
        {
            if (!HasKitchenObject)
            {
                if (Player.Instance!.HasKitchenObject &&
                    CheckAvailableRecipes(Player.Instance.KitchenObject.KitchenObjectSO))
                {
                    Player.Instance.KitchenObject.KitchenObjectParent = this;
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
            if (HasKitchenObject && CheckAvailableRecipes(KitchenObject.KitchenObjectSO))
            {
                KitchenObjectSO recipeOutput = GetSliceRecipeOutput(KitchenObject.KitchenObjectSO);
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(recipeOutput, this);
            }
        }

        private bool CheckAvailableRecipes(KitchenObjectSO kitchenObjectSO)
        {
            foreach (var sliceRecipe in sliceRecipesSO)
            {
                if (sliceRecipe.input == kitchenObjectSO)
                {
                    return true;
                }
            }

            return false;
        }

        private KitchenObjectSO GetSliceRecipeOutput(KitchenObjectSO inputSO)
        {
            foreach (var sliceRecipe in sliceRecipesSO)
            {
                if (sliceRecipe.input == inputSO)
                {
                    return sliceRecipe.output;
                }
            }

            return null;
        }
    }
}