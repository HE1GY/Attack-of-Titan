using System;
using OmniDirectionalMobilityFolder;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class Maneuvering : MonoBehaviour
    {
        /*public event Action<Vector3> Hooked;
        public event Action HookFire;*/

        public IHookableWaepon LeftWeapon=>_leftWeaponSlot;
        public IHookableWaepon RightWeapon=>_rightWeaponSlot;
        

        [Header("Visualization")]
        [SerializeField] private RopeVisualization _ropeVisualizationLeft;
        [SerializeField] private RopeVisualization _ropeVisualizationRight;


        [Header("Grapple")]
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private InputActionReference _leftRopeRaising;
        [SerializeField] private InputActionReference _rightRopeRaising;

        [SerializeField] private ActionBasedContinuousMoveProvider _continuousMove;
        [SerializeField] private Hand _leftHand;
        [SerializeField] private Hand _rightHand;


        [Header("Boosting")]
        [SerializeField] private Transform _camTransform;
        [SerializeField] private InputActionReference _boostAction;
        [SerializeField] private Rigidbody _rigidbody;
        

        private GrapplerGun _leftGrappler;
        private GrapplerGun _rightGrappler;

        private GasBoosting _gasBoosting;


        private IHookableWaepon _leftWeaponSlot;
        private IHookableWaepon _rightWeaponSlot;
        
        
        private bool _leftRising;
        private bool _rightRising;


        private void Awake()
        {
            _gasBoosting = new GasBoosting(_rigidbody, _camTransform);
            _leftGrappler = new GrapplerGun(_layerMask, _rigidbody.gameObject);
            _rightGrappler = new GrapplerGun(_layerMask, _rigidbody.gameObject);
            
            InteractionSubscribing();
        }
        
        private void Update()
        {
            if (_leftRising&&_leftGrappler.IsHooked)
            {
                _leftGrappler.Rising();
            }
            if (_rightRising&&_rightGrappler.IsHooked)
            {
                _rightGrappler.Rising();
            }
        }


        private void InteractionSubscribing()
        {
            RisingInputActionSubscribe();

            BoostInputActionSubscribe();

            HandleHandsEvents();
        }


        private void HandleHandsEvents()
        {
            _leftHand.TakeWeapon += weapon =>
            {
                SetupWeapon(weapon, out _leftWeaponSlot, _rightGrappler,_ropeVisualizationLeft);
            };

            _leftHand.DropItem += () =>
            {
                _leftWeaponSlot = null;
            };
            
            
            _rightHand.TakeWeapon += weapon =>
            {
                SetupWeapon(weapon, out _rightWeaponSlot, _rightGrappler,_ropeVisualizationRight);
            };
            _rightHand.DropItem += () =>
            {
                _rightWeaponSlot = null;
            };
        }

        private void SetupWeapon(IHookableWaepon weapon,out IHookableWaepon weaponSlot, GrapplerGun grapplerGun,RopeVisualization ropeVisualization)
        {
            weaponSlot = weapon;
            weaponSlot.Hooked += () =>
            {
                grapplerGun.StartGrappling(weapon.ShootPoint);

                ropeVisualization.SetSprintJoint(grapplerGun.Spring);
                ropeVisualization.SetShootPoint(weapon.ShootPoint);
                ropeVisualization.IsGrappling = true;
                
                CheckMoveType();
            };
            weaponSlot.UnHooked += () =>
            {
                ropeVisualization.IsGrappling = false;
                grapplerGun.StopGrappling();
                CheckMoveType();
            };
        }

        private void BoostInputActionSubscribe()
        {
            _boostAction.action.started += ctx =>
            {
                if (_leftGrappler.IsHooked || _rightGrappler.IsHooked)
                {
                    Vector2 input = ctx.ReadValue<Vector2>();
                    _gasBoosting.Boost(input);
                }
            };
        }

        private void RisingInputActionSubscribe()
        {
            _leftRopeRaising.action.started += ctx => _leftRising = true;
            _leftRopeRaising.action.canceled += ctx => _leftRising = false;

            _rightRopeRaising.action.started += ctx => _rightRising = true;
            _rightRopeRaising.action.canceled += ctx => _rightRising = false;
        }
        
        private void CheckMoveType()
        {
            _continuousMove.enabled =  !_leftGrappler.IsHooked && !_rightGrappler.IsHooked;
        }
    }
}