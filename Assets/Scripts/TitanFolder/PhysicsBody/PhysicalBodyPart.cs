using UnityEngine;

namespace TitanFolder.PhysicsBody
{
        [RequireComponent(typeof(ConfigurableJoint))]
        public class PhysicalBodyPart : MonoBehaviour
        {
                [SerializeField] private Transform _target;

                private ConfigurableJoint _configurable;
                private Quaternion _startRotation;

                private void Start()
                {
                        _configurable = GetComponent<ConfigurableJoint>();
                        _startRotation = transform.localRotation;
                }

                private void FixedUpdate()
                {
                        if (_configurable)
                        {
                                _configurable.targetRotation =
                                        Quaternion.Inverse(_target.localRotation) * _startRotation;
                        }
                }
        }
}
