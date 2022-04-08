using System;
using OmniDirectionalMobilityFolder;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class HandInteractor : XRDirectInteractor
    {
        public event Action<BladeInteractable> GetBlade;
        public event Action DropedItem;
    
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            GameObject selectedGameObject = args.interactableObject.transform.gameObject;
            TryGetBlade(selectedGameObject);
            base.OnSelectEntered(args);
        }


        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            DropedItem?.Invoke();
        }


        private void TryGetBlade(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out BladeInteractable blade))
            {
                GetBlade?.Invoke(blade);
            }
        }
    }
}
