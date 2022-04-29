using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TitanFolder.Body
{
    public class HitZone : XRSocketInteractor
    {
        public event Action DestroyPart;

        [Header("HIT ZONE")] [SerializeField] private ConfigurableJoint _configurableJoint;
        [SerializeField] private float _strength;
        [SerializeField] private ParticleSystem _hitEffect;


        protected override void Start()
        {
            allowSelect = false;
            base.Start();
        }

        private void OnBladeEnter()
        {
            _strength -= 1;
            if (_strength <= 0 && _configurableJoint)
            {
                _configurableJoint.breakForce = 0;
                DestroyPart?.Invoke();
            }
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args) // blade mask
        {
            base.OnHoverEntered(args);
            OnBladeEnter();
            _hitEffect.Play();
        }
    }
}