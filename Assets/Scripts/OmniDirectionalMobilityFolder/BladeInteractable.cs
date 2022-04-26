using System;
using TitanFolder.Body;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace OmniDirectionalMobilityFolder
{
    public class BladeInteractable :XRGrabInteractable
    {
        public event Action PlaySound;
        public event Action<Transform> Hook;
        public event Action UnHook;

        [SerializeField] private Transform _shootPoint;


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
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out HitZone hitZone))
            {
                PlaySound?.Invoke();
            }
        }
    }
    
}
