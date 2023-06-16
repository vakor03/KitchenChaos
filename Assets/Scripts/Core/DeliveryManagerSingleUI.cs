﻿#region

using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Core
{
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeNameText;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private Transform iconTemplate;

        public void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        public void SetRecipeSO(RecipeSO recipeSO)
        {
            recipeNameText.text = recipeSO.recipeName;

            foreach (Transform child in iconContainer)
            {
                if (child == iconTemplate)
                {
                    continue;
                }
                Destroy(child.gameObject);
            }
            
            foreach (var recipeSOIngredient in recipeSO.ingredients)
            {
                Transform iconTransform = Instantiate(iconTemplate, iconContainer);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<Image>().sprite = recipeSOIngredient.sprite;
            }
        }
    }
}