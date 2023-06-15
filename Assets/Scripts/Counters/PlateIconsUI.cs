using System;
using Core;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class PlateIconsUI : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private Transform iconTemplate;

        private void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            plateKitchenObject.OnAddIngredient += PlateKitchenObjectOnAddIngredient;
        }

        private void PlateKitchenObjectOnAddIngredient(KitchenObjectSO kitchenObject)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            ClearUI();
            foreach (var kitchenObjectSO in plateKitchenObject.KitchenObjectSOList)
            {
                Transform iconTransform = Instantiate(iconTemplate, transform);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<PlateSingleIconUI>().KitchenObjectSO = kitchenObjectSO;
            }
        }

        private void ClearUI()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child != iconTemplate)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}