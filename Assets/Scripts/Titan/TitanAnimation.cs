using System.Linq;
using UnityEngine;

namespace Titan
{
    public class TitanAnimation
    {
        private const float NormalizedTransitionDuration = 0.5f;
        private const int LayerIndex = 0;
        private readonly TitanAnimationHash _titanAnimationHash=new TitanAnimationHash();
        private readonly Animator _animator;

        public TitanAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void PlaySleeping()
        {
            _animator.CrossFade(_titanAnimationHash.SpleepingHash,NormalizedTransitionDuration);
        }

        public void PlayWalk()
        {
            _animator.CrossFade(_titanAnimationHash.WalkHash,NormalizedTransitionDuration);
        }

        public void PlayLegsAttack()
        {
            _animator.CrossFade(_titanAnimationHash.LegsAttackHash,NormalizedTransitionDuration);
        }
        public void PlayHandsAttack()
        {
            _animator.Play(_titanAnimationHash.HandsAttackHash);
        }
        public void PlaySteamAttack()
        {
            _animator.Play(_titanAnimationHash.SteamAttackHash);
        }

        public float GetCurrentAnimationDuration()
        {
            AnimationClip[] clipsInfo = _animator.runtimeAnimatorController.animationClips;

            float time=  clipsInfo[0].length;
            Debug.Log(time);
            return time;
        }
    }
}