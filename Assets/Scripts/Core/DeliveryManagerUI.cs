#region

using Counters;
using UnityEngine;

#endregion

namespace Core
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform recipeTemplate;

        private void Awake()
        {
            recipeTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManagerOnRecipeCompleted;
            DeliveryManager.Instance.OnRecipeSpawned += DeliveryManagerOnRecipeSpawned;

            UpdateVisual();
        }

        private void DeliveryManagerOnRecipeSpawned()
        {
            UpdateVisual();
        }

        private void DeliveryManagerOnRecipeCompleted()
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in container)
            {
                if (child == recipeTemplate)
                {
                    continue;
                }

                Destroy(child.gameObject);
            }

            foreach (var recipeSO in DeliveryManager.Instance.WaitingRecipeSOList)
            {
                Transform recipeTransform = Instantiate(recipeTemplate, container);
                recipeTransform.gameObject.SetActive(true);
                recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
            }
        }
    }
}