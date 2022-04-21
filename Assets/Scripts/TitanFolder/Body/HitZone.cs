using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Object = UnityEngine.Object;

namespace TitanFolder.Body
{
    public class HitZone : XRSocketInteractor
    {
        [Header("HIT ZONE")]
        [SerializeField] private ConfigurableJoint _configurableJoint;
        [SerializeField] private float _strength;
        [SerializeField] private ParticleSystem _hitEffect;

        private ConfigurableJoint _nextConfigurableJoint;
        private GameObject _jointHolder;// копіюй компонент
    
        public event Action Hit;
        

        protected override void Start()
        {
            _nextConfigurableJoint = new ConfigurableJoint();
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
            
            OnBladeEnter();
            _hitEffect.Play();
            Hit?.Invoke();
        }
        
    }
}
