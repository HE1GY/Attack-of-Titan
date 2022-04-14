using UnityEngine;

namespace Titan.PhysicsBody
{
    public class BodyRotator : MonoBehaviour
    {
        public Transform Target { get; set; }
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
            if (Target)
            {
                Vector3 direction = Target.position - _configurableJoint.transform.position;
                direction.y = 0;
                Quaternion rotation=Quaternion.LookRotation(direction);
            
                _configurableJoint.targetRotation=Quaternion.Inverse(rotation)*_startRotation;
            }
        }
    }
}
