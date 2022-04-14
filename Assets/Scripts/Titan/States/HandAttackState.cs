using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Titan.States
{
    public class HandAttackState:ITargetableState
    {
        private TitanAnimation _titanAnimation;
        private Rig _handsRig;

        public HandAttackState(TitanAnimation titanAnimation,Rig handsRig)
        {
            _handsRig = handsRig;
            _titanAnimation = titanAnimation;
        }


        public void Enter(Transform target)
        {
            _handsRig.GetComponent<TwoBoneIKConstraint>().data.target.position=target.position;
            Enter();
        }

        public void Enter()
        {
            _titanAnimation.PlayLegAttack();
        }

        public void UpdateState()
        {
            
        }

        public void Exite()
        {
            
        }
    }
}