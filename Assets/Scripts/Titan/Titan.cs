using Titan.PhysicsBody;
using UnityEngine;
using UnityEngine.Animations.Rigging;


namespace Titan
{
    public class Titan:MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private BodyRotator _bodyRotator;
        
        [Header("FOV")]
        [SerializeField]private LayerMask _obstacles;
        [SerializeField]private float _viewLength;
        [SerializeField]private float _fovAngle;
        [SerializeField]private Transform _eyesPosition;

        [Header("BodyTriggers")]
        [SerializeField]private BodyTrigger _legsScaner;
        [SerializeField]private BodyTrigger _handsScaner;
        [SerializeField]private BodyTrigger _shoulderScaner;

        [Header("Attack")] 
        [SerializeField] private ParticleSystem _steamPartical;
        [SerializeField] private Rig _handRig;

        
        
        private StateMachine _titanStateMachine;
        private TitanAnimation _titanAnimation;
        private FieldOfView _fieldOfView;
        private BodyScaner _bodyScaner;

        private void Awake()
        {
            _titanAnimation = new TitanAnimation(_animator);
            _fieldOfView = new FieldOfView(_obstacles, _viewLength, _fovAngle, _eyesPosition);
            
            _titanStateMachine = new StateMachine(_titanAnimation,_fieldOfView,_bodyRotator,_steamPartical,_handRig);
        }


        private void Update()
        {
            _titanStateMachine.UpdateState();
        }
    }

    public class BodyScaner
    {
        private StateMachine _stateMachine;
        
        private BodyTrigger _legsScaner;
        private BodyTrigger _handsScaner;
        private BodyTrigger _shoulderScaner;
        

    }
}