using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class Jumper : MonoBehaviour
    {
        private const float HoldTime=1;

        [SerializeField] private ActionBasedContinuousMoveProvider _continuousMove;
        [SerializeField]private InputActionReference _inputAction;
        [SerializeField] private float _maxForce;
        [SerializeField]private Rigidbody _rigidbody;



        private void Awake()
        {
            _inputAction.action.performed += ctx => Jump(_maxForce);
            _inputAction.action.canceled += ScaledJump;
        }

        private void Jump(float force)
        {
            if (_continuousMove.enabled)
            {
                _rigidbody.AddForce(Vector3.up*force,ForceMode.VelocityChange);
            }
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



