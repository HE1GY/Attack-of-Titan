using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    public class RopeVisualization: MonoBehaviour
    {
        [SerializeField]private LineRenderer _lineRenderer;
        [SerializeField]private AnimationCurve _affectCurve;
        [SerializeField] private Transform _shootPoint;
        
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
        public bool IsGrappling { get; set; }

        public void SetGrapplePoint(Vector3 point)
        {
            _grapplePoint = point;
        }

        public void Awake()
        {
            _visualizationSpring = new VisualizationSpring();
            _visualizationSpring.SetTarget(0);
        }

        private void LateUpdate()
        {
            DrawRope();
        }

      private  void DrawRope()
      {
          if (IsGrappling)
            {
                if (_lineRenderer.positionCount == 0) {
                    _visualizationSpring.SetVelocity(Velocity);
                    _lineRenderer.positionCount = Quality + 1;
                }
        
                _visualizationSpring.SetDamper(Damper);
                _visualizationSpring.SetStrength(Strength);
                _visualizationSpring.Update(Time.deltaTime);

                var grapplePoint = _grapplePoint;
                var gunTipPosition =  _shootPoint.position;
                var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

                _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, grapplePoint, Time.deltaTime * LerpSpeed);

                for (var i = 0; i < Quality + 1; i++) {
                    var delta = i / (float) Quality;
                    var offset = up * WaveHeight * Mathf.Sin(delta * WaveCount * Mathf.PI) * _visualizationSpring.Value *
                                 _affectCurve.Evaluate(delta);
            
                    _lineRenderer.SetPosition(i, Vector3.Lerp(gunTipPosition, _currentGrapplePosition, delta) + offset);
                }
            }
            else
            {
                _currentGrapplePosition = _shootPoint.position;;
                _visualizationSpring.Reset();
                if (_lineRenderer.positionCount > 0)
                    _lineRenderer.positionCount = 0;
            }
      }

    }
}