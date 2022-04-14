using UnityEngine;

namespace Titan
{
    public class TitanAnimation
    {
        private TitanAnimationHash _titanAnimationHash=new TitanAnimationHash();
        private Animator _animator;

        public TitanAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void PlaySleeping()
        {
            _animator.CrossFade(_titanAnimationHash.SpleepingHash,0.5f);
        }

        public void PlayWalk()
        {
            _animator.CrossFade(_titanAnimationHash.WalkHash,0.5f);
        }

        public void PlayLegAttack()
        {
            _animator.CrossFade(_titanAnimationHash.LegAttackHash,0.5f);
        }
        public void PlayHandAttack()
        {
            _animator.CrossFade(_titanAnimationHash.HandAttackHash,0.5f);
        }
        public void PlaySteamAttack()
        {
            _animator.CrossFade(_titanAnimationHash.SteamAttackHash,0.5f);
        }
    }
}