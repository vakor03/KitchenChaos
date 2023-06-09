using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    [SerializeField] [CanBeNull] private ClearCounter secondCounter;
    [SerializeField] private bool testing;

    // public bool HasKitchenObject => _kitchenObject != null;
    private KitchenObject _kitchenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            // _kitchenObject.ClearCounter = secondCounter;
        }
    }

    public void Interact()
    {
        if (_kitchenObject is null)
        {
            Transform kitchenObject = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
            kitchenObject.localPosition = Vector3.zero;
            _kitchenObject = kitchenObject.GetComponent<KitchenObject>();
        }
        
        
        
        Debug.Log("Interact");
    }
}