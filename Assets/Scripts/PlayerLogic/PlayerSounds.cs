using System;
using Core;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    public class PlayerSounds : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private float _footstepTimer;
        private float _footstepTimerMax = .1f;

        private void Update()
        {
            _footstepTimer += Time.deltaTime;
            if (_footstepTimer > _footstepTimerMax)
            {
                _footstepTimer = 0;
                if (_player.IsWalking())
                {
                    SoundManager.Instance.PlayFootstepSounds(_player.transform.position);
                }
            }
        }
    }
}