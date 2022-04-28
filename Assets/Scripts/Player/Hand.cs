using System;
using OmniDirectionalMobilityFolder;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    [RequireComponent(typeof(XRDirectInteractor))]
    public class Hand : MonoBehaviour
    {
        public event Action<IHookableWaepon> TakeWeapon;
        public event Action DropItem;
        
        public void Grab(SelectEnterEventArgs args)
        {
            GameObject selectedGameObject = args.interactableObject.transform.gameObject;
            Taking(selectedGameObject);
        }

        public void Drop()
        {
            DropItem?.Invoke();
        }
        
        private void Taking(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IHookableWaepon waepon))
            {
                TakeWeapon?.Invoke(waepon);
            }
        }
    }
}