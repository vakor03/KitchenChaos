using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public event Action OnStateChanged;

        public bool IsGamePlaying => _currentState == State.GamePlaying;
        public bool IsCountdownTimerActive => _currentState == State.CountdownToStart;

        public float GetCountdownToStartTimer()
        {
            return _countdownToStartTimer;
        }

        private enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }

        private State _currentState;
        private float _waitingToStartTimer = 3f;
        private float _countdownToStartTimer = 5f;
        private float _gamePlayingTimer = 10f;

        private void Awake()
        {
            _currentState = State.WaitingToStart;
            Instance = this;
        }

        private void Update()
        {
            switch (_currentState)
            {
                case State.WaitingToStart:
                    _waitingToStartTimer -= Time.deltaTime;
                    if (_waitingToStartTimer < 0f)
                    {
                        _currentState = State.CountdownToStart;
                        OnStateChanged?.Invoke();
                    }

                    break;
                case State.CountdownToStart:
                    _countdownToStartTimer -= Time.deltaTime;
                    if (_countdownToStartTimer < 0f)
                    {
                        _currentState = State.GamePlaying;
                        OnStateChanged?.Invoke();
                    }

                    break;
                case State.GamePlaying:
                    _gamePlayingTimer -= Time.deltaTime;
                    if (_gamePlayingTimer < 0f)
                    {
                        _currentState = State.GameOver;
                        OnStateChanged?.Invoke();
                    }

                    break;
                case State.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}