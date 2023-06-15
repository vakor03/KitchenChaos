#region

using PlayerLogic;
using UnityEngine;

#endregion

namespace Counters
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private GameObject[] visualGameObjectArray;

        private void Start()
        {
            Player.Instance!.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
        }

        private void PlayerOnSelectedCounterChanged(BaseCounter selectedCounter)
        {
            if (selectedCounter == baseCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Hide()
        {
            foreach (var visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(false);
            }
        }

        private void Show()
        {
            foreach (var visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(true);
            }
        }
    }
}