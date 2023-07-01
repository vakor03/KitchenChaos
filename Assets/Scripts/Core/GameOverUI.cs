#region

using Counters;
using TMPro;
using UnityEngine;

#endregion

namespace Core
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipesDeliveredText;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;
            
            Hide();
        }

        private void GameManagerOnStateChanged()
        {
            if (GameManager.Instance.IsGameOver)
            {
                Show();
                recipesDeliveredText.text = DeliveryManager.Instance.RecipesDelivered.ToString();

            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}