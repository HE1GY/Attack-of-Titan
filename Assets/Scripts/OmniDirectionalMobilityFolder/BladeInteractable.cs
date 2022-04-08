using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace OmniDirectionalMobilityFolder
{
    public class BladeInteractable :XRGrabInteractable
    {
        public event Action<Transform> Hook;
        public event Action UnHook;
        
        [SerializeField] private Transform _shootPoint;
        

        public void Damage(float damage)
        {
        
        }


        protected override void OnActivated(ActivateEventArgs args)
        {
            base.OnActivated(args);
            Hook?.Invoke(_shootPoint);
        }

        protected override void OnDeactivated(DeactivateEventArgs args)
        {
            base.OnDeactivated(args);
            UnHook?.Invoke();
        }
    }
}