using System;
using UnityEngine;

namespace Titan.PhysicsBody
{
    public class ColliderCollision : MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders1;
        [SerializeField] private Collider[] _colliders2;

        private void Awake()
        {
            foreach (var collider in _colliders1)
            {
                foreach (var collider2 in _colliders2)
                {
                    Physics.IgnoreCollision(collider,collider2,true);
                }
            }
        }
    }
}
