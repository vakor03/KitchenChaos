#region

using UnityEngine;

#endregion

namespace Core
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject visualGameObject;

        private void Start()
        {
            Player.Instance!.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
        }

        private void PlayerOnSelectedCounterChanged(ClearCounter selectedCounter)
        {
            if (selectedCounter == clearCounter)
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
            visualGameObject.SetActive(false);
        }

        private void Show()
        {
            visualGameObject.SetActive(true);
        }
    }
}