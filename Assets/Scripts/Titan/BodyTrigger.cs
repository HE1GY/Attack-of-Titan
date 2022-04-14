using System;
using Player;
using UnityEngine;

namespace Titan
{
    public class BodyTrigger : MonoBehaviour
    {
        public event Action<Transform> PlayerNear;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Maneuvering maneuvering))
            {
                PlayerNear?.Invoke(maneuvering.transform);
            }
        }
    }
}
