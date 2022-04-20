using UnityEngine;
using Zenject;

namespace Titan.PhysicsBody
{
    public class BodyRotator : MonoBehaviour
    {
        public Transform Target { get; set; }
        
        private const float TurnSpeed = 0.00001f;
        private ConfigurableJoint _configurableJoint;
        private Quaternion _startRotation;
        private float _interpolationRatio;
        
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

                if (_interpolationRatio < 1)
                {
                    _interpolationRatio += Time.fixedDeltaTime*TurnSpeed;
                    _configurableJoint.targetRotation= Quaternion.Slerp( _configurableJoint.targetRotation ,Quaternion.Inverse(rotation)*_startRotation,_interpolationRatio);
                }
                else
                {
                    _interpolationRatio = 0;
                }
            }
        }
    }
}
