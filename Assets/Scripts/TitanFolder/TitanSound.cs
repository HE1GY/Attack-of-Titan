using UnityEngine;

namespace TitanFolder
{
    [RequireComponent(typeof(AudioSource))]
    public class TitanSound : MonoBehaviour
    {
         [SerializeField] private Body.Body body;

        [SerializeField] private AudioClip _fall;
        [SerializeField] private AudioClip _spawned;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _audioSource.PlayOneShot(_spawned);
            body.Touch += PlayFall;
        }

        private void OnDisable()
        {
            body.Touch -= PlayFall;
        }


        private void PlayFall()
        {
            _audioSource.PlayOneShot(_fall);
        }
    }
}
