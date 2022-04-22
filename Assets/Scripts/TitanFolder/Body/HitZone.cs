using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TitanFolder.Body
{
    public class HitZone : XRSocketInteractor
    {
        public event Action<ConfigurableJoint> Hit;

        [Header("HIT ZONE")]
        [SerializeField] private ConfigurableJoint _configurableJoint;
        
        public ConfigurableJoint CurrentJoint { get=>_configurableJoint; set=>_configurableJoint=value;}

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
            if (_strength <= 0&& _configurableJoint)
            {
                _configurableJoint.breakForce = 0;
            }
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            
            Hit?.Invoke(_configurableJoint);
            OnBladeEnter();
            _hitEffect.Play();
        }
        
    }
}
