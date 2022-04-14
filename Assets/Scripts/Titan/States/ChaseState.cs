using Titan.PhysicsBody;
using UnityEngine;

namespace Titan.States
{
    class ChaseState : ITargetableState
    {
        private readonly TitanAnimation _titanAnimation;
        private readonly BodyRotator _bodyRotator;
        private readonly FieldOfView _fieldOfView;
        private readonly StateMachine _stateMachine;

        public ChaseState(TitanAnimation titanAnimation, BodyRotator bodyRotator, FieldOfView fieldOfView, StateMachine stateMachine)
        {
            _titanAnimation = titanAnimation;
            _bodyRotator = bodyRotator;
            _fieldOfView = fieldOfView;
            _stateMachine = stateMachine;
        }

        public void Enter(Transform target)
        {
            _bodyRotator.Target = target;
            Enter();
        }

        public void Enter()
        {
            _fieldOfView.NotDetected += OnNotDetected;
            _titanAnimation.PlayWalk();
        }

        public void UpdateState()
        {
            _fieldOfView.HandleDetecting();
        }

        public void Exite()
        {
            _bodyRotator.Target = null;
            _fieldOfView.NotDetected -= OnNotDetected;
        }

        private void OnNotDetected()
        {
            _stateMachine.EnterState<SleepingState>();
        }
        
    }
}