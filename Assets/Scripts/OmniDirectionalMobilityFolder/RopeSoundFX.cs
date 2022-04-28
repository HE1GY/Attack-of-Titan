using System;
using Player;
using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    [RequireComponent(typeof(AudioSource))]
    public class RopeSoundFX : MonoBehaviour
    {
        [SerializeField] private Maneuvering _maneuvering;

        [SerializeField] private AudioClip _raising;
        [SerializeField] private AudioClip _grappled;
        [SerializeField] private AudioClip _hookFire;

        private AudioSource _audioSource;

        private bool _isRasing;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /*private void OnEnable()
        {
            _maneuvering.HookFire += OnHookFire;
        }*/

        private void OnHookFire()
        {
            _audioSource.clip = _hookFire;
            _audioSource.Play();
        }


        private void Update()
        {
            /*HandleRising();*/
        }
        

        /*private void HandleRising()
        { 
            if (_isRasing != (_maneuvering._leftRising || _maneuvering._rightRising))
            {
                if (_maneuvering._leftRising || _maneuvering._rightRising)
                {
                    PlayRising();
                    _isRasing = true;
                }
                else
                {
                    StopPlay();
                    _isRasing = false;
                }
            }
        }*/


        private void PlayRising()
        {
            _audioSource.loop = true;
            _audioSource.clip = _raising;
            _audioSource.Play();
        }

        private void StopPlay()
        {
            _audioSource.Stop();
        }
    }
}
