using UnityEngine;

namespace OmniDirectionalMobilityFolder.SoundFX
{
    [RequireComponent(typeof(AudioSource))]
    public class ManeuveringSound : MonoBehaviour
    {
        [SerializeField] private Maneuvering _maneuvering;

        [SerializeField] private AudioClip _ropeLock;
        [SerializeField] private AudioClip _gasBoost;
        [SerializeField] private AudioClip _hookFire;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _maneuvering.LeftHand.TakeWeapon += weapon => { weapon.Hooked += OnHookFire; };
            _maneuvering.RightHand.TakeWeapon += weapon => { weapon.Hooked += OnHookFire; };

            _maneuvering.RopeVisualizationLeft.Lock += OnLock;
            _maneuvering.RopeVisualizationRight.Lock += OnLock;

            _maneuvering.Boost += OnBoost;
        }


        private void OnHookFire()
        {
            _audioSource.PlayOneShot(_hookFire);
        }

        private void OnLock(Vector3 lockPos)
        {
            AudioSource.PlayClipAtPoint(_ropeLock, lockPos);
        }

        private void OnBoost()
        {
            _audioSource.PlayOneShot(_gasBoost);
        }
    }
}