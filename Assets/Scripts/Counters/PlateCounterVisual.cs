#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Counters
{
    public class PlateCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlateCounter plateCounter;
        [SerializeField] private Transform platePrefab;
        [SerializeField] private Transform counterTopPoint;

        private int _platesCount;
        private List<Transform> _platesVisuals;

        private void Awake()
        {
            _platesVisuals = new List<Transform>();
        }

        private void Start()
        {
            plateCounter.OnPlateSpawned += PlateCounterOnPlateSpawned;
            plateCounter.OnPlateRemoved += PlateCounterOnPlateRemoved;
        }

        private void PlateCounterOnPlateRemoved()
        {
            Destroy(_platesVisuals[^1].gameObject);
            _platesCount--;
            _platesVisuals.RemoveAt(_platesVisuals.Count - 1);
        }

        private void PlateCounterOnPlateSpawned()
        {
            Transform plateTransform = Instantiate(platePrefab, counterTopPoint);
            _platesVisuals.Add(plateTransform);
            float plateOffsetY = 0.1f;
            plateTransform.localPosition = new Vector3(0, plateOffsetY * _platesCount++, 0);
        }
    }
}