using UnityEngine;

namespace Titan.States
{
    class SleepingState : IState
    {
        private readonly TitanAnimation _titanAnimation;
        private readonly FieldOfView _fieldOfView;
        private readonly StateMachine _stateMachine;

        public SleepingState(TitanAnimation titanAnimation, FieldOfView fieldOfView, StateMachine stateMachine)
        {
            _titanAnimation = titanAnimation;
            _fieldOfView = fieldOfView;
            _stateMachine = stateMachine;
            
        }

        public void Enter()
        {
            _titanAnimation.PlaySleeping();
            _fieldOfView.Detected+= OnDetection;
        }

        public void UpdateState()
        {
            _fieldOfView.HandleDetecting();
        }

        public void Exite()
        {
            _fieldOfView.Detected-= OnDetection;
        }


        private void OnDetection(Transform target)
        {
            _stateMachine.EnterState<ChaseState>(target);
        }
    }
}