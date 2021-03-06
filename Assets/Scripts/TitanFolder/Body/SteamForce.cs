using UnityEngine;

namespace TitanFolder.Body
{
    public class SteamForce : MonoBehaviour
    {
        [SerializeField] private float _force;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(_force * transform.forward);
            }
        }
    }
}