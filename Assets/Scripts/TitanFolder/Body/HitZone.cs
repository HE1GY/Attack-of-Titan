using System;
using OmniDirectionalMobilityFolder;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TitanFolder.Body
{
    public class HitZone : XRSocketInteractor
    {
        [Header("HIT ZONE")]
        [SerializeField] private ConfigurableJoint _configurableJoint;
        [SerializeField] private float _strength;

        protected override void Start()
        {
            this.allowSelect = false;
            base.Start();
        }

        public void OnBladeEnter()
        {
            _strength -= 1;
            if (_strength <= 0&& _configurableJoint)
            {
                _configurableJoint.breakForce = 0;
                print("Hit");
            }
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            OnBladeEnter();
        }
    }
}
