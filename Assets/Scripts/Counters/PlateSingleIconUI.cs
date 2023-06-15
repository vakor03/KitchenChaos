using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Counters
{
    public class PlateSingleIconUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        public KitchenObjectSO KitchenObjectSO {
            set => icon.sprite = value.sprite;
        }
    }
}