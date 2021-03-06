using OmniDirectionalMobilityFolder.Visualization;
using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    public class GrapplerGun
    {
        public bool IsHooked { get; private set; }

        private const float MaxDistance = 100;
        private const int RiseDistance = 1;
        private const int RaiseSpeed = 15;
        private const int SpringJointMassScale = 50;
        private const float SpringJointSpring = 0.5f;
        private const int SpringJointBreak = 100;

        private readonly LayerMask _layerMask;
        private readonly GameObject _player;
        private readonly RopeVisualization _ropeVisualization;

        private Transform _shootPoint;
        private SpringJoint _springJoint;


        public GrapplerGun(LayerMask layerMask, GameObject player, RopeVisualization ropeVisualization)
        {
            _layerMask = layerMask;
            _player = player;
            _ropeVisualization = ropeVisualization;
        }

        public void StartGrappling(Transform shootPointTransform)
        {
            IsHooked = true;
            _shootPoint = shootPointTransform;
            if (_springJoint == null)
            {
                _springJoint = _player.AddComponent<SpringJoint>();
            }

            if (TryGetTargetRaycastHit(out RaycastHit targetRaycastHit))
            {
                _springJoint.autoConfigureConnectedAnchor = false;
                if (targetRaycastHit.rigidbody)
                {
                    _springJoint.connectedBody = targetRaycastHit.rigidbody;
                    _springJoint.connectedAnchor =
                        targetRaycastHit.rigidbody.transform.InverseTransformPoint(targetRaycastHit.point);
                }
                else
                {
                    _springJoint.connectedAnchor = targetRaycastHit.point;
                }

                _springJoint.maxDistance = Vector3.Distance(targetRaycastHit.point, _shootPoint.position);
                _springJoint.massScale = SpringJointMassScale;
                _springJoint.spring = SpringJointSpring;
                _springJoint.breakForce = SpringJointBreak;
                _springJoint.breakTorque = SpringJointBreak;

                _ropeVisualization.SetSprintJoint(_springJoint);
                _ropeVisualization.SetShootPoint(shootPointTransform);
                _ropeVisualization.Visualize(true);
            }
        }

        public void StopGrappling()
        {
            IsHooked = false;
            if (_springJoint)
            {
                _springJoint.maxDistance = Mathf.Infinity;
                _springJoint.connectedBody = null;

                _ropeVisualization.Visualize(false);
            }
        }

        public void Rising()
        {
            if (IsHooked && _shootPoint &&
                Vector3.Distance(_springJoint.connectedAnchor, _shootPoint.position) > RiseDistance)
            {
                _springJoint.maxDistance -= Time.fixedDeltaTime * RaiseSpeed;
            }
        }

        private bool TryGetTargetRaycastHit(out RaycastHit targetRaycastHit)
        {
            {
                Vector3 rayOrg = _shootPoint.position;
                Vector3 rayDir = _shootPoint.forward;
                Ray ray = new Ray(rayOrg, rayDir);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxDistance, _layerMask))
                {
                    targetRaycastHit = raycastHit;
                    return true;
                }

                targetRaycastHit = new RaycastHit();
                return false;
            }
        }
    }
}