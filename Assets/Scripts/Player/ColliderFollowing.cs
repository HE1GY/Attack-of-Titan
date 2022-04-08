using Unity.XR.CoreUtils;
using UnityEngine;

namespace Player
{
    public class ColliderFollowing : MonoBehaviour
    {
        [SerializeField]private float _additionHeight;
    
        private CapsuleCollider _collider;
        private XROrigin _xrOrigin;

        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
            _xrOrigin = GetComponent<XROrigin>();
        }

        private void Update()
        {
            CapsuleFollowHeadSet();
        }
        private void CapsuleFollowHeadSet()
        {
            _collider.height = _xrOrigin.CameraInOriginSpaceHeight + _additionHeight;
            Vector3 capsuleCenter = transform.InverseTransformPoint(_xrOrigin.Camera.transform.position);
            _collider.center = new Vector3(capsuleCenter.x, _collider.height / 2, capsuleCenter.z);
        }
    }
}
