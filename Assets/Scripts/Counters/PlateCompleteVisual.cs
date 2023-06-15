#region

using System;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Counters
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjects;

        private void Start()
        {
            plateKitchenObject.OnAddIngredient += PlateKitchenObjectOnAddIngredient;
            
            ClearPlate();
        }

        private void PlateKitchenObjectOnAddIngredient(KitchenObjectSO kitchenObject)
        {
            foreach (var kitchenObjectSOGameObject in kitchenObjectSOGameObjects)
            {
                if (kitchenObject == kitchenObjectSOGameObject.kitchenObjectSO)
                {
                    kitchenObjectSOGameObject.gameObject.SetActive(true);
                }
            }
        }

        private void ClearPlate()
        {
            foreach (var kitchenObjectSOGameObject in kitchenObjectSOGameObjects)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(false);
            }
        }

        // ReSharper disable once InconsistentNaming
        [Serializable]
        private struct KitchenObjectSO_GameObject
        {
            public KitchenObjectSO kitchenObjectSO;
            public GameObject gameObject;
        }
    }
}