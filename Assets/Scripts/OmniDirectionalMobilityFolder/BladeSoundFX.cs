using System;
using OmniDirectionalMobilityFolder;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(AudioSource))]
    public class BladeSoundFX : MonoBehaviour
    {
        [SerializeField] private AudioClip _hitSFX;

        [SerializeField] private BladeInteractable _bladeInteractable;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _bladeInteractable.PlaySound += OnHit;
        }

        private void OnDisable()
        {
            _bladeInteractable.PlaySound -= OnHit;
        }

        private void OnHit()
        {
            _audioSource.clip = _hitSFX;
            _audioSource.Play();
        }
        

    }

}
