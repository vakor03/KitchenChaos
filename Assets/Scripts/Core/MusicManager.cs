using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        private AudioSource _audioSource;
        private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

        private void Awake()
        {
            Instance = this;

            _audioSource = GetComponent<AudioSource>();

            _currentVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
            _audioSource.volume = _currentVolume;

        }

        private float _currentVolume = .3f;
        public void ChangeVolume()
        {
            _currentVolume += 0.1f;
            if (_currentVolume>1f)
            {
                _currentVolume = 0f;
            }

            _audioSource.volume = _currentVolume;
            
            PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, _currentVolume);
            PlayerPrefs.Save();
        }

        public float GetVolume()
        {
            return _currentVolume;
        }
    }
}