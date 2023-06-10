#region

using System;
using Core;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Counters
{
    public class StoveCounter : BaseCounter
    {
        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }

        [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
        [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
        private float _burningTimer;
        private BurningRecipeSO _currentBurningRecipeSO;

        private FryingRecipeSO _currentFryingRecipeSO;
        private State _currentState;
        private float _fryingTimer;

        public void Start()
        {
            _currentState = State.Idle;
        }

        private void Update()
        {
            if (HasKitchenObject)
            {
                switch (_currentState)
                {
                    case State.Idle:
                        break;
                    case State.Frying:
                        RunFryingState();
                        break;
                    case State.Fried:
                        RunFriedState();
                        break;
                    case State.Burned:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public event Action<State> OnStateChanged;

        private void RunFryingState()
        {
            _fryingTimer += Time.deltaTime;
            if (_fryingTimer > _currentFryingRecipeSO.fryingTimerMax)
            {
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(_currentFryingRecipeSO.output, this);

                _currentState = State.Fried;

                _currentBurningRecipeSO = GetBurningRecipeWithInput(KitchenObject.KitchenObjectSO);
                OnStateChanged?.Invoke(_currentState);
            }
        }

        private void RunFriedState()
        {
            _burningTimer += Time.deltaTime;
            
            if (_burningTimer > _currentBurningRecipeSO.burningTimerMax)
            {
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(_currentBurningRecipeSO.output, this);

                _currentState = State.Burned;
                OnStateChanged?.Invoke(_currentState);
            }
        }

        public override void Interact()
        {
            if (!HasKitchenObject)
            {
                if (Player.Player.Instance!.HasKitchenObject &&
                    TryGetFryingRecipeWithInput(Player.Player.Instance.KitchenObject.KitchenObjectSO,
                        out FryingRecipeSO fryingRecipeSO))
                {
                    Player.Player.Instance.KitchenObject.KitchenObjectParent = this;
                    _currentFryingRecipeSO = fryingRecipeSO;

                    _currentState = State.Frying;
                    OnStateChanged?.Invoke(_currentState);
                    _fryingTimer = 0f;
                    _burningTimer = 0f;
                }
            }
            else
            {
                if (!Player.Player.Instance!.HasKitchenObject)
                {
                    KitchenObject.KitchenObjectParent = Player.Player.Instance;

                    _currentState = State.Idle;
                    OnStateChanged?.Invoke(_currentState);
                }
            }
        }

        private bool TryGetFryingRecipeWithInput(KitchenObjectSO inputSO, out FryingRecipeSO outFryingRecipeSO)
        {
            foreach (var fryingRecipeSO in fryingRecipeSOArray)
            {
                if (fryingRecipeSO.input == inputSO)
                {
                    outFryingRecipeSO = fryingRecipeSO;
                    return true;
                }
            }

            outFryingRecipeSO = null;
            return false;
        }

        private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectSO inputSO)
        {
            foreach (var burningRecipeSO in burningRecipeSOArray)
            {
                if (burningRecipeSO.input == inputSO)
                {
                    return burningRecipeSO;
                }
            }

            return null;
        }
    }
}