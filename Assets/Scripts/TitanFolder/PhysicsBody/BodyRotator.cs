using System;
using TitanFolder.Body;
using UnityEngine;

namespace TitanFolder.PhysicsBody
{
    public class BodyRotator : MonoBehaviour
    {

        [SerializeField] private HitZone _neckZone;
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

        private void OnEnable()
        {
            _neckZone.DestroyPart +=OnDestroy;
        }

        private void FixedUpdate()
        {
            if (Target)
            {
                Vector3 direction = Target.position - _configurableJoint.transform.position;
                direction.y = 0;
                Quaternion rotation=Quaternion.LookRotation(direction);

                Rotate(rotation);
            }
            

        }

        private void Rotate(Quaternion rotation)
        {
            if (_interpolationRatio < 1)
            {
                _interpolationRatio += Time.fixedDeltaTime * TurnSpeed;
                _configurableJoint.targetRotation = Quaternion.Slerp(_configurableJoint.targetRotation,
                    Quaternion.Inverse(rotation) * _startRotation, _interpolationRatio);
            }
            else
            {
                _interpolationRatio = 0;
            }
        }

        private void OnDestroy()
        {
            _configurableJoint.slerpDrive = new JointDrive();
        }
    }
}
