using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Jumper : MonoBehaviour
    {
        private const float HoldTime=1;
    
        [SerializeField]private InputActionReference _inputAction;
        [SerializeField] private float _maxForce;
    
        private Rigidbody _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inputAction.action.performed += ctx => Jump(_maxForce);
            _inputAction.action.canceled += ScaledJump;
        }

        private void Jump(float force)
        {
            _rigidbody.AddForce(Vector3.up*force,ForceMode.VelocityChange);
        }

        private void ScaledJump(InputAction.CallbackContext ctx)
        {
            if (ctx.duration > HoldTime) return;
        
            float pressTime=(float)ctx.duration;
            float calculatedForce = pressTime * _maxForce / HoldTime;
            Jump(calculatedForce);
        }
    
    }
}



