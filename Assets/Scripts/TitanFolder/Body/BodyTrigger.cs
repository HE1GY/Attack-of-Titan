using System;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace TitanFolder.Body
{
    public class BodyTrigger : MonoBehaviour
    {
        public event Action<Transform> EnemyNear;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out XROrigin xrOrigin))
            {
                EnemyNear?.Invoke(xrOrigin.transform);
            }
        }
    }
}