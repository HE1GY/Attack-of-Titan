using TitanFolder.Body;
using TitanFolder.PhysicsBody;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace TitanFolder
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
        [SerializeField]private BodyTrigger _legsScanner;
        [SerializeField]private BodyTrigger _handsScanner;
        [SerializeField]private BodyTrigger _shoulderScanner;

        [Header("Attack")]
        [SerializeField] private Rig _handRig;

        
        
        private StateMachine _titanStateMachine;
        private TitanAnimation _titanAnimation;
        private FieldOfView _fieldOfView;
        private AttackScanner _attackScanner;

        private void Awake()
        {
            _titanAnimation = new TitanAnimation(_animator);
            _fieldOfView = new FieldOfView(_obstacles, _viewLength, _fovAngle, _eyesPosition);
            _titanStateMachine = new StateMachine(_titanAnimation,_fieldOfView,_bodyRotator,_handRig);
            _attackScanner = new AttackScanner(_legsScanner, _handsScanner, _shoulderScanner,_titanStateMachine);
        }


        private void Update()
        {
            _titanStateMachine.UpdateState();
        }

        public class Factory:PlaceholderFactory<Titan>{}
        
    }
}