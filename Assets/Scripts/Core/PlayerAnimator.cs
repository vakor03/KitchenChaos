﻿#region

using UnityEngine;

#endregion

namespace Core
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private const string IS_WALKING = "IsWalking";

        [SerializeField] private Player player;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(IS_WALKING, player.IsWalking());
        }
    }
}