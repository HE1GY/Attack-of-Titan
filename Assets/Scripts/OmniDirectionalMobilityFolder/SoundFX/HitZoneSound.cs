using System;
using UnityEngine;

namespace OmniDirectionalMobilityFolder.SoundFX
{
    [RequireComponent(typeof(AudioSource))]
    public class BladeSound : MonoBehaviour
    {
        
        [SerializeField] private AudioClip _cutTitan;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        
        
    }
}
