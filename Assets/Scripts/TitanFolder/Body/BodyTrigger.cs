using System;
using Player;
using UnityEngine;

namespace TitanFolder.Body
{
    public class BodyTrigger : MonoBehaviour
    {
        public event Action<Transform> EnemyNear;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Maneuvering maneuvering))
            {
                EnemyNear?.Invoke(maneuvering.transform);
            }
        }
    }
}
