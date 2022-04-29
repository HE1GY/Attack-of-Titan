using System;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace TitanFolder
{
    public class FieldOfView
    {
        public event Action<Transform> Detected; 
        public event Action NotDetected; 
        
        private readonly LayerMask _obstacles;
        private readonly float _viewLength;
        private readonly float _fovAngle;
        private readonly Transform _eyesPosition;

        public FieldOfView(LayerMask obstacles, float viewLength, float fovAngle, Transform eyesPosition)
        {
            _obstacles = obstacles;
            _viewLength = viewLength;
            _fovAngle = fovAngle;
            _eyesPosition = eyesPosition;
        }


        public void HandleDetecting()
        {
            Collider[] colliders = Physics.OverlapSphere(_eyesPosition.position,_viewLength);

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out XROrigin xROrigin))
                {
                    Vector3 direction = xROrigin.transform.position - _eyesPosition.position;
                    if (Vector3.Angle(direction, -_eyesPosition.forward) < _fovAngle/2)
                    {
                        Ray ray = new Ray(_eyesPosition.position, direction);
                        float distanceBetween =
                            Vector3.Distance(_eyesPosition.position, xROrigin.transform.position);
                        if (!Physics.Raycast(ray, distanceBetween, _obstacles))
                        {
                            Detected?.Invoke(xROrigin.transform);
                            return;
                        }
                    }
                }
            }
            NotDetected?.Invoke();
        }
    }
}