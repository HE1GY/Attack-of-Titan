using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    public class AimVisualization : MonoBehaviour
    {
        private const float Distance = 100;
        private const float Offset = 0.01f;
        private const int ScaleFactor = 2;

        [SerializeField] private Transform _shootPoint;
        [SerializeField]private LayerMask _layerMask;
        [SerializeField] private Transform _visual;


        private bool _isShowing;

        public void SetIsShowing(bool isShowing)
        {
            _isShowing = isShowing;
        }
        private void Update()
        {
            if (_isShowing)
            {
                Vector3 rayOrg=_shootPoint.position;
                Vector3 rayDir=_shootPoint.forward;
                Ray ray = new Ray(rayOrg, rayDir);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, Distance, _layerMask))
                {
                    if (!_visual.gameObject.activeInHierarchy)
                    {
                        TurnOnSight();
                    }
               
                    _visual.position = raycastHit.point+raycastHit.normal.normalized*Offset;
                    _visual.forward = -raycastHit.normal;
                    _visual.localScale =
                        Vector3.Distance(transform.position, raycastHit.point) * _shootPoint.localScale*ScaleFactor;
                }
                else
                {
                    if (_visual.gameObject.activeInHierarchy)
                    {
                        TurnOffSight();
                    }
                }
            }
            else
            {
                if (_visual.gameObject.activeInHierarchy)
                {
                    TurnOffSight();
                }
                
            }

        }


        public void TurnOnSight()
        {
            _visual.gameObject.SetActive(true);
        }
        public void TurnOffSight()
        {
            _visual.gameObject.SetActive(false);
        }
    }
}
