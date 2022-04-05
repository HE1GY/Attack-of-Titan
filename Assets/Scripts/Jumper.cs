using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Jumper : MonoBehaviour
{
    [SerializeField]private InputActionReference _inputAction;
    private Rigidbody _rigidbody;
    [SerializeField] private float _force;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputAction.action.performed += ctx => Jump();
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up*_force,ForceMode.Acceleration);
    }
}



