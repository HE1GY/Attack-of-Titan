using System;
using OmniDirectionalMobilityFolder;
using UnityEngine;

namespace Titan
{
    public class HitZone : MonoBehaviour
    {
        [SerializeField] private ConfigurableJoint _configurableJoint;
        [SerializeField] private float _strength;
        

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out BladeInteractable bladeInteractable))
            {
                _strength -= 1;

                if (_strength <= 0&& _configurableJoint)
                {
                    _configurableJoint.breakForce = 0;
                }
            }
        }
    }
}
