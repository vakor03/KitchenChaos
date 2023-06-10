#region

using UnityEngine;
using static Counters.StoveCounter;

#endregion

namespace Counters
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        [SerializeField] private GameObject stoveOnVisualsGameObject;
        [SerializeField] private GameObject particlesGameObject;

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
        }

        private void StoveCounterOnStateChanged(State state)
        {
            bool showVisual = state == State.Frying || state == State.Fried;
            stoveOnVisualsGameObject.SetActive(showVisual);
            particlesGameObject.SetActive(showVisual);
        }
    }
}