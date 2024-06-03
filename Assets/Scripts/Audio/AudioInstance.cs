using UnityEngine;

namespace Audio {
    public class AudioInstance 
    {
        private AudioManager _audioManager;
        private AudioType _audioType;
        private AudioSource _audioSourceCache;
        private float _startVolume;
        
        public AudioInstance(AudioSource audioSource, AudioType audioType)
        {
            _startVolume = audioSource.volume;
            _audioSourceCache = audioSource;
            _audioType = audioType;
            
        }

        public void Initialize(AudioManager audioManager)
        {
            _audioManager = audioManager;
            _audioManager.OnAudioChange[(int)_audioType] += ChangeVolume;
            ChangeVolume(_audioManager.IsAudioActive[(int)_audioType]);
        }

        public void Play()
        {
            _audioSourceCache.Play();
        }

        private void ChangeVolume(bool isEnable)
        {
            _audioSourceCache.volume = isEnable ? _startVolume : 0f;
        }
    }
    
}

