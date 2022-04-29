using System;
using OmniDirectionalMobilityFolder.Visualization;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace OmniDirectionalMobilityFolder
{
    public class Maneuvering : MonoBehaviour
    {
        public event Action Boost;
        public Hand LeftHand => _leftHand;
        public Hand RightHand => _rightHand;

        public RopeVisualization RopeVisualizationLeft => _ropeVisualizationLeft;
        public RopeVisualization RopeVisualizationRight => _ropeVisualizationRight;


        [Header("Visualization")] [SerializeField]
        private RopeVisualization _ropeVisualizationLeft;

        [SerializeField] private RopeVisualization _ropeVisualizationRight;


        [Header("Grapple")] [SerializeField] private LayerMask _layerMask;

        [SerializeField] private InputActionReference _leftRopeRaising;
        [SerializeField] private InputActionReference _rightRopeRaising;

        [SerializeField] private ActionBasedContinuousMoveProvider _continuousMove;
        [SerializeField] private Hand _leftHand;
        [SerializeField] private Hand _rightHand;


        [Header("Boosting")] [SerializeField] private Transform _camTransform;
        [SerializeField] private InputActionReference _boostAction;
        [SerializeField] private Rigidbody _rigidbody;


        private GrapplerGun _leftGrappler;
        private GrapplerGun _rightGrappler;

        private GasBoosting _gasBoosting;


        private IHookableWeapon _leftWeaponSlot;
        private IHookableWeapon _rightWeaponSlot;


        private bool _leftRising;
        private bool _rightRising;


        private void Awake()
        {
            _gasBoosting = new GasBoosting(_rigidbody, _camTransform);
            _leftGrappler = new GrapplerGun(_layerMask, _rigidbody.gameObject, _ropeVisualizationLeft);
            _rightGrappler = new GrapplerGun(_layerMask, _rigidbody.gameObject, _ropeVisualizationRight);

            InteractionSubscribing();
        }

        private void FixedUpdate()
        {
            if (_leftRising)
            {
                _leftGrappler.Rising();
            }

            if (_rightRising)
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
            _leftHand.TakeWeapon += weapon => { SetupWeapon(weapon, out _leftWeaponSlot, _leftGrappler); };

            _leftHand.DropItem += () =>
            {
                _leftWeaponSlot.ResetWeapon();
                _leftWeaponSlot = null;
            };


            _rightHand.TakeWeapon += weapon => { SetupWeapon(weapon, out _rightWeaponSlot, _rightGrappler); };
            _rightHand.DropItem += () =>
            {
                _rightWeaponSlot.ResetWeapon();
                _rightWeaponSlot = null;
            };
        }

        private void SetupWeapon(IHookableWeapon weapon, out IHookableWeapon weaponSlot, GrapplerGun grapplerGun)
        {
            weaponSlot = weapon;
            weaponSlot.Hooked += () =>
            {
                grapplerGun.StartGrappling(weapon.ShootPoint);
                CheckMoveType();
            };
            weaponSlot.UnHooked += () =>
            {
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
                    Boost?.Invoke();
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
            _continuousMove.enabled = !_leftGrappler.IsHooked && !_rightGrappler.IsHooked;
        }
    }
}