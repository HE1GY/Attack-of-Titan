using UnityEngine;

namespace TitanFolder.Body
{
    [RequireComponent(typeof(AudioSource))]
    public class HitZoneSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _cutting;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayCut()
        {
            _audioSource.PlayOneShot(_cutting);
        }
    }
}