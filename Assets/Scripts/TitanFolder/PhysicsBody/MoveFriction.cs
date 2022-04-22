using UnityEngine;

namespace TitanFolder.PhysicsBody
{
    public class MoveFriction : MonoBehaviour
    {
        [SerializeField] private Collider _leftCollider;
        [SerializeField] private Collider _rightCollider;
        
        [SerializeField] private PhysicMaterial _zeroFriction;
        [SerializeField] private PhysicMaterial _defaultFriction;

        private void LeftStep()
        {
            _leftCollider.material = _defaultFriction;
            _rightCollider.material = _zeroFriction;
        }
        
        private void RightStep()
        {
            _leftCollider.material = _zeroFriction;
            _rightCollider.material = _defaultFriction;
        }
    }
}
