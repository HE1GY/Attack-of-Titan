using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    public class GrapplerGun
    {
        private readonly Transform _shootPoint;
        private readonly float _maxDistance;
        private readonly LayerMask _layerMask;
        private readonly Transform _player;
        private  SpringJoint _springJoint;

        private Transform _pointer;//Test 
        private RopeVisualization _ropeVisualization;
        

        public GrapplerGun(Transform shootPoint, float maxDistance, LayerMask layerMask, Transform player, Transform pointer,RopeVisualization ropeVisualization)
        {
            _shootPoint = shootPoint;
            _maxDistance = maxDistance;
            _layerMask = layerMask;
            _player = player;
            _pointer = pointer;

            _ropeVisualization =ropeVisualization ;
        }

        public void StartGrappling()
        {
            if (TryGetTarget(out Vector3 target))
            {
                if (_springJoint == null)
                {
                    _springJoint=_player.gameObject.AddComponent<SpringJoint>();
                }
                _pointer.position = target;//test
                
                _springJoint.autoConfigureConnectedAnchor = false;
                _springJoint.connectedAnchor = target;
                _springJoint.maxDistance =Vector3.Distance(target, _shootPoint.position);
                
                _ropeVisualization.SetGrapplePoint(target);
                _ropeVisualization.IsGrappling = true;
            }
        }

        public void StopGrappling()
        {
            _springJoint.maxDistance = Mathf.Infinity;
            _ropeVisualization.IsGrappling = false;
        }

        private bool TryGetTarget(out Vector3 targetPos)
        {
            {
                Vector3 rayOrg = _shootPoint.position;
                Vector3 rayDir = _shootPoint.forward;
                Ray ray = new Ray(rayOrg, rayDir);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, _maxDistance, _layerMask))
                {
                    targetPos = raycastHit.point;
                    return true;
                }

                targetPos = Vector3.zero;
                return false;
            }
        }
    }
}