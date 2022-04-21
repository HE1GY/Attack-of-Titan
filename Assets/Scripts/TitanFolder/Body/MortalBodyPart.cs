using System.Threading.Tasks;
using UnityEngine;

namespace TitanFolder.Body
{
    public class MortalBodyPart : MonoBehaviour
    {
        [SerializeField] private HitZone _hitZone;
        [SerializeField] private float _delay;
        [SerializeField] private ParticleSystem _deathEffect;

        [SerializeField]private Transform[] _parents;
        

        private void OnEnable()
        {
            _hitZone.Hit += Death;
        }

        private void OnDisable()
        {
            _hitZone.Hit -= Death;
        }


        private async void Death()
        {
            await DelayDeath();
        }

        private async  Task DelayDeath()
        {
            await Task.Delay((int)_delay*1000/2);
            _deathEffect.Play();
            
            await Task.Delay((int)_delay*1000/2);
            foreach (var parent in _parents)
            {
                parent.gameObject.SetActive(false);
            }
            _deathEffect.Stop();
        }
    }
}
