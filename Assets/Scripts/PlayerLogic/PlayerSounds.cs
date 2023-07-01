#region

using Core;
using UnityEngine;

#endregion

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    public class PlayerSounds : MonoBehaviour
    {
        private float _footstepTimer;
        private float _footstepTimerMax = .1f;
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

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