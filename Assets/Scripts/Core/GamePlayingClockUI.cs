#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Core
{
    public class GamePlayingClockUI : MonoBehaviour
    {
        [SerializeField] private Image timerImage;

        private void Update()
        {
            timerImage.fillAmount = GameManager.Instance.GetPlayingTimerNormalized();
        }
    }
}