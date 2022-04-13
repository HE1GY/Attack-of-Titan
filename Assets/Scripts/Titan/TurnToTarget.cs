using System;
using UnityEngine;

namespace Titan
{
    public class TurnToTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private ConfigurableJoint _configurableJoint;

        private Quaternion _startRotation;

        private void Awake()
        {
            _configurableJoint = GetComponent<ConfigurableJoint>();
            _startRotation = _configurableJoint.targetRotation;
            _startRotation.y = 180;
        }

        private void FixedUpdate()
        {
            Vector3 direction = _target.position - _configurableJoint.transform.position;
            direction.y = 0;
            Quaternion rotation=Quaternion.LookRotation(direction);
            
            _configurableJoint.targetRotation=Quaternion.Inverse(rotation)*_startRotation;
        }
    }
}
