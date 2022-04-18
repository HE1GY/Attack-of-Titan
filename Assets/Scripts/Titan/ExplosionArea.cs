using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Titan.PhysicsBody
{
    public class ExplosionArea : MonoBehaviour
    {
        private const int DestroyTime = 3;
        private const int ExplosionRadius = 5;
        private const float ExplosionScaler=25;

        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private LayerMask _exclusionMask;
        
         private Rigidbody _rigidbody;

         private void Awake()
         {
             _rigidbody = GetComponent<Rigidbody>();
         }

         public void OnCollisionEnter(Collision collision)
        {
            foreach (var contact in collision.contacts)
            {
                GameObject particle=  Instantiate(_explosionEffect.gameObject, contact.point, Quaternion.identity);
                MakeBoom(contact.point);
                Destroy(particle,DestroyTime);
            }
        }

        private void MakeBoom(Vector3 position)
        {
            float explosionForce = _rigidbody.velocity.magnitude * ExplosionScaler;
            
            Collider[] colliders = Physics.OverlapSphere(position,ExplosionRadius,~_exclusionMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(explosionForce, position, explosionForce);
                }
            }
        }
    }
}