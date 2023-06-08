using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private Player player;

        private const string IS_WALKING = "IsWalking";

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