using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace OmniDirectionalMobilityFolder
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class Blade:MonoBehaviour,IHookableWaepon
    {
        [SerializeField] private Transform _shootPoint;
        public Transform ShootPoint { get=>_shootPoint;}
        public event Action Hooked;
        public event Action UnHooked;

        public void Hook()
        {
            Hooked?.Invoke();
        }

        public void UnHook()
        {
            UnHooked?.Invoke();
        }
        
    }
}