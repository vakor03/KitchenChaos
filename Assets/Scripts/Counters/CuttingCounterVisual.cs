#region

using UnityEngine;

#endregion

namespace Counters
{
    [RequireComponent(typeof(Animator))]
    public class CuttingCounterVisual : MonoBehaviour
    {
        private const string CUT = "Cut";
        private static readonly int CutHash = Animator.StringToHash(CUT);
        [SerializeField] private CuttingCounter cuttingCounter;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            cuttingCounter.OnCut += CuttingCounterOnCut;
        }

        private void CuttingCounterOnCut()
        {
            _animator.SetTrigger(CutHash);
        }
    }
}