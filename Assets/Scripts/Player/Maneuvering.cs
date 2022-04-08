using System;
using OmniDirectionalMobilityFolder;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class Maneuvering : MonoBehaviour
    {
        [SerializeField] private RopeVisualization _ropeVisualizationLeft;
        [SerializeField] private RopeVisualization _ropeVisualizationRight;
        
        [SerializeField] private LayerMask _layerMask;
        
        [SerializeField] private ActionBasedContinuousMoveProvider _continuousMove;

        [SerializeField] private HandInteractor _leftHand;
        [SerializeField] private HandInteractor _rightHand;

        [Header("Boosting")]
        [SerializeField] private Transform _camTransform;
        [SerializeField] private InputActionReference _boostAction;

        [SerializeField] private InputActionReference _leftRopeRaising;
        [SerializeField] private InputActionReference _rightRopeRaising;

        private BladeInteractable _leftBlade;
        private BladeInteractable _rightBlade;
        
        private GrapplerGun _leftGrappler;
        private GrapplerGun _rightGrappler;
        private GasBoosting _gasBoosting;

        private Rigidbody _rigidbody;

        private bool _leftHooked;
        private bool _rightHooked;

        private bool _leftRising;
        private bool _rightRising;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _gasBoosting = new GasBoosting(_rigidbody, _camTransform);
            _leftGrappler = new GrapplerGun(_layerMask, gameObject, _ropeVisualizationLeft);
            _rightGrappler = new GrapplerGun(_layerMask, gameObject, _ropeVisualizationRight);
            
            InteractionSubscribing();
            
        }

        private void Update()
        {
            if (_leftRising)
            {
                _leftGrappler.Raising();
            }
            else if (_rightRising)
            {
                _rightGrappler.Raising();
            }
        }


        private void InteractionSubscribing()
        {
            _leftRopeRaising.action.performed += ctx => _leftRising = ctx.ReadValueAsButton();
            _rightRopeRaising.action.performed += ctx => _rightRising = ctx.ReadValueAsButton();
            
            _boostAction.action.started +=ctx=>
            {
                Vector2 input = ctx.ReadValue<Vector2>();
                _gasBoosting.Boost(input);
            };
                
            _leftHand.GetBlade += blade =>
            {
                _leftBlade = blade;
                _leftBlade.Hook +=shootPoint=>
                {
                    _leftGrappler.StartGrappling(shootPoint);
                    _ropeVisualizationLeft.SetShootPoint(shootPoint);
                    _leftHooked = true;
                    CheckMoveType();
                };
                _leftBlade.UnHook += ()=>
                {
                    _leftGrappler.StopGrappling();
                    _leftHooked = false;
                    CheckMoveType();
                };
            };
            _rightHand.GetBlade += blade =>
            {
                _rightBlade = blade;
                _rightBlade.Hook +=shootPoint=>
                {
                    _ropeVisualizationRight.SetShootPoint(shootPoint);
                    _rightGrappler.StartGrappling(shootPoint);
                    _rightHooked = true;
                    CheckMoveType();
                };
                _rightBlade.UnHook +=()=>
                {
                    _rightGrappler.StopGrappling();
                    _rightHooked = false;
                    CheckMoveType();
                };
            };

            _leftHand.DropedItem += () => _leftBlade = null;
            _rightHand.DropedItem += () => _rightBlade = null;
        }


        private void CheckMoveType()
        {
            _continuousMove.enabled = !_leftHooked && !_rightHooked;
        }
    }
}