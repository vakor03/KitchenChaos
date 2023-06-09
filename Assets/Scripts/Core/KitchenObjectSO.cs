using System;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "KitchenObjectsSO")]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public String objName;
    }
}