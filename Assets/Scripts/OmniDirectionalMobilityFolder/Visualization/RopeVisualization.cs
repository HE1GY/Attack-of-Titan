using System;
using UnityEngine;

namespace OmniDirectionalMobilityFolder.Visualization
{
    public class RopeVisualization : MonoBehaviour
    {
        public event Action<Vector3> Lock;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private AnimationCurve _affectCurve;
        private Transform _shootPoint;

        private VisualizationSpring _visualizationSpring;

        private const int Quality = 500;
        private const float Damper = 14;
        private const float Strength = 800;
        private const float Velocity = 15;
        private const float WaveCount = 3;
        private const float WaveHeight = 1;
        private const float LerpSpeed = 6f;

        private Vector3 _grapplePoint;
        private Vector3 _currentGrapplePosition;
        private SpringJoint _springJoint;
        private bool _isGrappling;
        private bool _isLock;

        public void Visualize(bool yes)
        {
            _isGrappling = yes;
        }

        public void SetSprintJoint(SpringJoint springJoint)
        {
            _springJoint = springJoint;
        }

        public void SetShootPoint(Transform shootPoint)
        {
            _shootPoint = shootPoint;
        }

        public void Awake()
        {
            _visualizationSpring = new VisualizationSpring();
            _visualizationSpring.SetTarget(0);
            _lineRenderer.positionCount = 0;
        }

        private void LateUpdate()
        {
            if (_springJoint)
            {
                if (_springJoint.connectedBody)
                {
                    _grapplePoint = _springJoint.connectedBody.transform.TransformPoint(_springJoint.connectedAnchor);
                }
                else
                {
                    _grapplePoint = _springJoint.connectedAnchor;
                }
            }

            if (_shootPoint)
            {
                DrawRope();
            }
        }

        private void DrawRope()
        {
            if (_isGrappling)
            {
                if (_lineRenderer.positionCount == 0)
                {
                    _visualizationSpring.SetVelocity(Velocity);
                    _lineRenderer.positionCount = Quality + 1;
                    _currentGrapplePosition = _shootPoint.position;
                }

                _visualizationSpring.SetDamper(Damper);
                _visualizationSpring.SetStrength(Strength);
                _visualizationSpring.Update(Time.deltaTime);

                Vector3 grapplePoint = _grapplePoint;
                Vector3 gunTipPosition = _shootPoint.position;
                Vector3 up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

                float t = Time.deltaTime * LerpSpeed;
                _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, grapplePoint, t);

                for (var i = 0; i < Quality + 1; i++)
                {
                    float delta = i / (float) Quality;
                    Vector3 offset = up * WaveHeight * Mathf.Sin(delta * WaveCount * Mathf.PI) *
                                     _visualizationSpring.Value *
                                     _affectCurve.Evaluate(delta);
                    _lineRenderer.SetPosition(i, Vector3.Lerp(gunTipPosition, _currentGrapplePosition, delta) + offset);
                    if (i == Quality && !_isLock)
                    {
                        Lock?.Invoke(_currentGrapplePosition);
                        _isLock = true;
                    }
                }
            }
            else
            {
                _currentGrapplePosition = _shootPoint.position;
                _visualizationSpring.Reset();
                _isLock = false;
                if (_lineRenderer.positionCount > 0)
                    _lineRenderer.positionCount = 0;
            }
        }
    }
}