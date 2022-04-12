using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    public class GrapplerGun
    {
        private const float MaxDistance=100;
        private const int RiseDistance = 1;
        private const int RaiseSpeed = 15;
        private const int SpringJointMassScale = 50;
        private const int SpringJointSpring = 1;

        private readonly LayerMask _layerMask;
        private readonly GameObject _player;

        private  Transform _shootPoint;
        private  SpringJoint _springJoint;
        private RopeVisualization _ropeVisualization;


        public GrapplerGun( LayerMask layerMask, GameObject player,RopeVisualization ropeVisualization)
        {
            _layerMask = layerMask;
            _player = player;

            _ropeVisualization =ropeVisualization ;
        }

        public void StartGrappling(Transform shootPointTransform)
        {
            _shootPoint = shootPointTransform;
            
            if (TryGetTarget(out Vector3 targetPosition))
            {
                if (_springJoint == null)
                {
                    _springJoint=_player.AddComponent<SpringJoint>();
                }
                _springJoint.autoConfigureConnectedAnchor = false;
                _springJoint.connectedAnchor = targetPosition;
                _springJoint.maxDistance =Vector3.Distance(targetPosition, _shootPoint.position);
                _springJoint.massScale =SpringJointMassScale;
                _springJoint.spring = SpringJointSpring;

                _ropeVisualization.SetSprintJoint(_springJoint);
                _ropeVisualization.IsGrappling = true;
            }
        }

        public void StopGrappling()
        {
            if (_springJoint)
            {
                _springJoint.maxDistance = Mathf.Infinity;
            }
            _ropeVisualization.IsGrappling = false;
        }

        public void Raising()
        {
            if (_shootPoint && Vector3.Distance(_springJoint.connectedAnchor, _shootPoint.position) > RiseDistance)
            {
                _springJoint.maxDistance -= Time.deltaTime*RaiseSpeed;
            }
        }

        private bool TryGetTarget(out Vector3 targetPosition)
        {
            {
                Vector3 rayOrg = _shootPoint.position;
                Vector3 rayDir = _shootPoint.forward;
                Ray ray = new Ray(rayOrg, rayDir);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxDistance, _layerMask))
                {
                    if (raycastHit.rigidbody)
                    {
                        _springJoint.connectedBody = raycastHit.rigidbody;
                    }
                    targetPosition = raycastHit.point;
                    return true;
                }

                targetPosition = Vector3.zero;
                return false;
            }
        }
    }
}