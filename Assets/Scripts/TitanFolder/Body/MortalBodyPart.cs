using System.Threading.Tasks;
using UnityEngine;

namespace TitanFolder.Body
{
    public class MortalBodyPart : MonoBehaviour
    {
        [SerializeField] private HitZone _hitZone;
        [SerializeField] private float _delay;
        [SerializeField] private ParticleSystem _deathEffect;

        [SerializeField] private Transform[] _parents;


        private void OnEnable()
        {
            _hitZone.DestroyPart += Death;
        }

        private void OnDisable()
        {
            _hitZone.DestroyPart -= Death;
        }


        private async void Death()
        {
            await DelayDeath();
        }

        private async Task DelayDeath()
        {
            await Task.Delay((int) _delay / 2 * 1000);
            _deathEffect.Play();

            await Task.Delay((int) _delay / 2 * 1000);

            foreach (var parent in _parents)
            {
                parent.gameObject.SetActive(false);
            }

            _deathEffect.Stop();
        }
    }
}