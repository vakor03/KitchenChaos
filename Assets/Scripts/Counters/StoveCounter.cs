#region

using System;
using Core;
using PlayerLogic;
using ScriptableObjects;
using UnityEngine;

#endregion

namespace Counters
{
    public class StoveCounter : BaseCounter, IHasProgress
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

        public event Action<float> OnProgressChanged;

        public event Action<State> OnStateChanged;


        private void RunFryingState()
        {
            _fryingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(_fryingTimer/_currentFryingRecipeSO.fryingTimerMax);
            if (_fryingTimer > _currentFryingRecipeSO.fryingTimerMax)
            {
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(_currentFryingRecipeSO.output, this);

                _currentState = State.Fried;

                _currentBurningRecipeSO = GetBurningRecipeWithInput(KitchenObject.KitchenObjectSO);
                OnStateChanged?.Invoke(_currentState);
                OnProgressChanged?.Invoke(0f);
            }
        }

        private void RunFriedState()
        {
            _burningTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(_burningTimer/_currentBurningRecipeSO.burningTimerMax);
            
            if (_burningTimer > _currentBurningRecipeSO.burningTimerMax)
            {
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(_currentBurningRecipeSO.output, this);

                _currentState = State.Burned;
                OnStateChanged?.Invoke(_currentState);
                OnProgressChanged?.Invoke(0f);
            }
        }

        public override void Interact()
        {
            if (!HasKitchenObject)
            {
                if (Player.Instance!.HasKitchenObject &&
                    TryGetFryingRecipeWithInput(Player.Instance.KitchenObject.KitchenObjectSO,
                        out FryingRecipeSO fryingRecipeSO))
                {
                    Player.Instance.KitchenObject.KitchenObjectParent = this;
                    _currentFryingRecipeSO = fryingRecipeSO;

                    _currentState = State.Frying;
                    OnStateChanged?.Invoke(_currentState);
                    OnProgressChanged?.Invoke(0f);
                    _fryingTimer = 0f;
                    _burningTimer = 0f;
                }
            }
            else
            {
                if (!Player.Instance!.HasKitchenObject)
                {
                    KitchenObject.KitchenObjectParent = Player.Instance;

                    _currentState = State.Idle;
                    OnStateChanged?.Invoke(_currentState);
                    OnProgressChanged?.Invoke(0f);
                }
                else
                {
                    if (Player.Instance!.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSO))
                        {
                            KitchenObject.DestroySelf();
                            
                            _currentState = State.Idle;
                            OnStateChanged?.Invoke(_currentState);
                            OnProgressChanged?.Invoke(0f);
                        }
                    }
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