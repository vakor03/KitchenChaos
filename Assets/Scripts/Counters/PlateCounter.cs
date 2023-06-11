#region

using System;
using Core;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Counters
{
    public class PlateCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
        
        private float _plateSpawnMaxTimer = 4f;
        private float _plateSpawnTimer;
        private int _platesSpawnedCount;
        private int _platesSpawnedMaxCount = 4;

        private void Update()
        {
            _plateSpawnTimer += Time.deltaTime;
            if (_plateSpawnTimer > _plateSpawnMaxTimer)
            {
                if (_platesSpawnedCount < _platesSpawnedMaxCount)
                {
                    OnPlateSpawned?.Invoke();
                    _platesSpawnedCount++;
                }

                _plateSpawnTimer = 0;
            }
        }

        public event Action OnPlateSpawned;
        public event Action OnPlateRemoved;

        public override void Interact()
        {
            if (!Player.Player.Instance!.HasKitchenObject)
            {
                if (_platesSpawnedCount > 0)
                {
                    _platesSpawnedCount--;
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, Player.Player.Instance);
                    OnPlateRemoved?.Invoke();
                }
            }
        }
    }
}