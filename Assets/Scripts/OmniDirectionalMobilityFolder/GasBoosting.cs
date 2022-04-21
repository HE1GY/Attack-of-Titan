using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    public class GasBoosting
    {
        private const float BoostVelocity = 15f;
        private Rigidbody _rigidbody;
        private Transform _camTrasform;

        public GasBoosting(Rigidbody rigidbody, Transform camTrasform)
        {
            _rigidbody = rigidbody;
            _camTrasform = camTrasform;
        }


        public void Boost(Vector2 direction)
        {
            Vector3 forceDirection = _camTrasform.forward * direction.y + _camTrasform.right * direction.x;
            forceDirection.Normalize();
            _rigidbody.velocity = forceDirection * BoostVelocity;
        }

    }
}