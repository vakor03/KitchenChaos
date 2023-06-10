#region

using System;
using UnityEngine;

#endregion

namespace Core
{
    [CreateAssetMenu(menuName = "KitchenObjectsSO")]
    public class KitchenObjectSO : ScriptableObject
    {
        public String objName;
        public Transform prefab;
        public Sprite sprite;
    }
}