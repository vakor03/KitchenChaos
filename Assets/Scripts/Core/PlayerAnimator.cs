#region

using UnityEngine;

#endregion

namespace Core
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private const string IS_WALKING = "IsWalking";
        private static readonly int IsWalkingHash = Animator.StringToHash(IS_WALKING);

        [SerializeField] private Player player;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(IsWalkingHash, player.IsWalking());
        }
    }
}