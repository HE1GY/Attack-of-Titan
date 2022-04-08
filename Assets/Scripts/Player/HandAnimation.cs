using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class HandAnimation : MonoBehaviour
    {
        private static readonly int TriggerHash = Animator.StringToHash("Trigger");
        private static readonly int GribHash = Animator.StringToHash("Grib");
    
        private Animator _animator;
        private ActionBasedController _controller;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponentInParent<ActionBasedController>();
        
            _controller.activateActionValue.action.performed += UpdateTriggerValue;
            _controller.selectActionValue.action.performed += UpdateGrabValue;
        }

        private void UpdateTriggerValue(InputAction.CallbackContext ctx)
        {
            _animator.SetFloat(TriggerHash,ctx.ReadValue<float>());
        }

        private void UpdateGrabValue(InputAction.CallbackContext ctx)
        {
            _animator.SetFloat(GribHash,ctx.ReadValue<float>());
        }
    }
}
