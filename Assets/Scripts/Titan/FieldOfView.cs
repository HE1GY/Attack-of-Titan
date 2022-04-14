using System;
using Player;
using UnityEngine;

namespace Titan
{
    public class FieldOfView
    {
        public event Action<Transform> Detected; 
        public event Action NotDetected; 
        
        private LayerMask _obstacles;
        private float _viewLength;
        private float _fovAngle;
        private Transform _eyesPosition;

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
                if (collider.gameObject.TryGetComponent(out Maneuvering maneuvering))
                {
                    Vector3 direction = maneuvering.transform.position - _eyesPosition.position;
                    if (Vector3.Angle(direction, -_eyesPosition.forward) < _fovAngle)
                    {
                        Ray ray = new Ray(_eyesPosition.position, direction);
                        float distanceBetween =
                            Vector3.Distance(_eyesPosition.position, maneuvering.transform.position);
                        if (!Physics.Raycast(ray, distanceBetween, _obstacles))
                        {
                            Detected?.Invoke(maneuvering.transform);
                            return;
                        }
                    }
                }
            }
            NotDetected?.Invoke();
        }
    }
}