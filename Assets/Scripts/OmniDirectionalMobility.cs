using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OmniDirectionalMobility : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _player;

    [SerializeField] private Transform _pointer;

    private SpringJoint _springJoint;
    


    public void StartGrappling()
    {
        if (TryGetTarget(out Vector3 target))
        {
            if (_springJoint == null)
            {
                _springJoint = _player.gameObject.AddComponent<SpringJoint>();
            }
            _pointer.position = target;
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = target;
            _springJoint.maxDistance = 0.5f*Vector3.Distance(target, transform.position);
            _springJoint.damper = 4.5f;
            _springJoint.spring = 50f;
            _springJoint.massScale = 4.5f;
            
        }
    }

    public void StopGrappling()
    {
        _springJoint.spring = 0;
    }

    private bool TryGetTarget(out Vector3 targetPos)
    {
        Vector3 rayOrg = _shootPoint.position;
        Vector3 rayDir= _shootPoint.forward;
        Ray ray = new Ray(rayOrg, rayDir);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, _maxDistance, _layerMask))
        {
            targetPos = raycastHit.point;
            return true;
        }
        targetPos=Vector3.zero;
        return false;
    }
}
