#region

using System;
using UnityEngine;

#endregion

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private float _countdownToStartTimer = 5f;

        private State _currentState;
        private float _gamePlayingTimer;
        private float _gamePlayingTimerMax = 30f;
        private float _waitingToStartTimer = 3f;
        public static GameManager Instance { get; private set; }

        public bool IsGamePlaying => _currentState == State.GamePlaying;
        public bool IsCountdownTimerActive => _currentState == State.CountdownToStart;
        public bool IsGameOver => _currentState == State.GameOver;

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
                        _gamePlayingTimer = _gamePlayingTimerMax;
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

        public event Action OnStateChanged;

        public float GetCountdownToStartTimer()
        {
            return _countdownToStartTimer;
        }

        public float GetPlayingTimerNormalized()
        {
            return (_gamePlayingTimerMax - _gamePlayingTimer) / _gamePlayingTimerMax;
        }

        private enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }
    }
}