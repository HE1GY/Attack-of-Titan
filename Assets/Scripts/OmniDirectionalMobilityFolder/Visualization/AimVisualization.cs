using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace OmniDirectionalMobilityFolder
{
    public class AimVisualization : MonoBehaviour
    {
        
        private const float Distance = 100;
        private const int ScaleFactor = 1;

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _visual;

        private bool _isHooked;



        public void SetIsHooked(bool hooked)
        {
            _isHooked = hooked;
        }


        
        
        private void Update()
        {
            if (!_isHooked)
            {
                Vector3 rayOrg = _shootPoint.position;
                Vector3 rayDir = _shootPoint.forward;
                Ray ray = new Ray(rayOrg, rayDir);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, Distance, _layerMask))
                {
                    if (!_visual.gameObject.activeInHierarchy)
                    {
                        TurnOnSign();
                    }

                    _visual.position = raycastHit.point /*+ raycastHit.normal.normalized *//** Offset*/;
                    _visual.up = raycastHit.normal;
                    _visual.localScale =
                        Vector3.Distance(transform.position, raycastHit.point) * _shootPoint.localScale * ScaleFactor;
                }
                else
                {
                    if (_visual.gameObject.activeInHierarchy)
                    {
                        TurnOffSign();
                    }
                }
            }
            else
            {
                if (_visual.gameObject.activeInHierarchy)
                {
                    TurnOffSign();
                }
            }
               
        }


        private void TurnOnSign()
        {
            _visual.gameObject.SetActive(true);
        }

        private void TurnOffSign()
        {
            _visual.gameObject.SetActive(false);
        }
    }
}