#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace Counters
{
    public class PlateCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlateCounter plateCounter;
        [SerializeField] private Transform platePrefab;
        [SerializeField] private Transform counterTopPoint;
        private List<Transform> _plates;

        private int _platesCount;

        private void Awake()
        {
            _plates = new List<Transform>();
        }

        private void Start()
        {
            plateCounter.OnPlateSpawned += PlateCounterOnPlateSpawned;
            plateCounter.OnPlateRemoved += PlateCounterOnPlateRemoved;
        }

        private void PlateCounterOnPlateRemoved()
        {
            Destroy(_plates[^1].gameObject);
            _platesCount--;
            _plates.RemoveAt(_plates.Count - 1);
        }

        private void PlateCounterOnPlateSpawned()
        {
            Transform plateTransform = Instantiate(platePrefab, counterTopPoint);
            _plates.Add(plateTransform);
            float plateOffsetY = 0.1f;
            plateTransform.localPosition = new Vector3(0, plateOffsetY * _platesCount++, 0);
        }
    }
}