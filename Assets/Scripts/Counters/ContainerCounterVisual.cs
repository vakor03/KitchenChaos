#region

using UnityEngine;

#endregion

namespace Counters
{
    [RequireComponent(typeof(Animator))]
    public class ContainerCounterVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";
        private static readonly int OpenCloseHash = Animator.StringToHash(OPEN_CLOSE);
        [SerializeField] private ContainerCounter containerCounter;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnItemGrabbed += ContainerCounterOnOnItemGrabbed;
        }

        private void ContainerCounterOnOnItemGrabbed()
        {
            _animator.SetTrigger(OpenCloseHash);
        }
    }
}