using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace TitanFolder.States
{
    public class AttackState:ITargetableState
    {
        private const int CoolDown = 1000;
        private const int MilliSeconds = 1000;
        
        private readonly TitanAnimation _titanAnimation;
        private readonly Rig _handRig;
        private readonly StateMachine _stateMachine;

        public AttackState(TitanAnimation titanAnimation, Rig handRig, StateMachine stateMachine)
        {
            _titanAnimation = titanAnimation;
            _handRig = handRig;
            _stateMachine = stateMachine;
        }

        public void Enter(Transform target)
        {
            if (target)
            {
                _handRig.GetComponentInChildren<TwoBoneIKConstraint>().data.target.position = target.position;
                _titanAnimation.PlayHandsAttack();
                ExiteAfterAnimation(_titanAnimation.GetCurrentAnimationDuration());
            }
            else
            {
                _titanAnimation.PlaySteamAttack();
                ExiteAfterAnimation(_titanAnimation.GetCurrentAnimationDuration());
            }
           
        }

        public void Enter()
        {
            _titanAnimation.PlayLegsAttack();
            ExiteAfterAnimation(_titanAnimation.GetCurrentAnimationDuration());
        }

        public void UpdateState()
        {
        }

        public void Exite()
        {
        }



        private async void ExiteAfterAnimation(float clipLength)
        {
            await Timer(clipLength);
            _stateMachine.EnterState<SleepingState>();
        }
        
        private async Task Timer(float duration)
        {
            await Task.Delay((int)duration*MilliSeconds+CoolDown);
        }
    }
}