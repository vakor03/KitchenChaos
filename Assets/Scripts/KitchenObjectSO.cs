using System;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    [field:SerializeField]public Transform Prefab { get; private set; }
    [field:SerializeField]public Sprite Sprite { get; private set; }
    [field:SerializeField]public String Name { get; private set; }
}