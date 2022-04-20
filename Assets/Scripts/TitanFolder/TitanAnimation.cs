using System.Linq;
using UnityEngine;

namespace TitanFolder
{
    public class TitanAnimation
    {
        private const float NormalizedTransitionDuration = 0.5f;
        private readonly TitanAnimationHash _titanAnimationHash=new TitanAnimationHash();
        private readonly Animator _animator;

        private int _currentAnimationHash;

        public TitanAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void PlaySleeping()
        {
            _animator.CrossFade(_titanAnimationHash.SpleepingHash,NormalizedTransitionDuration);
            _currentAnimationHash = _titanAnimationHash.SpleepingHash;
        }

        public void PlayWalk()
        {
            _animator.CrossFade(_titanAnimationHash.WalkHash,NormalizedTransitionDuration);
            _currentAnimationHash = _titanAnimationHash.WalkHash;
        }

        public void PlayLegsAttack()
        {
            _animator.CrossFade(_titanAnimationHash.LegsAttackHash,NormalizedTransitionDuration);
            _currentAnimationHash = _titanAnimationHash.LegsAttackHash;
        }
        public void PlayHandsAttack()
        {
            _animator.Play(_titanAnimationHash.HandsAttackHash);
            _currentAnimationHash = _titanAnimationHash.HandsAttackHash;
        }
        public void PlaySteamAttack()
        {
            _animator.Play(_titanAnimationHash.SteamAttackHash);
            _currentAnimationHash = _titanAnimationHash.SteamAttackHash;
        }

        public float GetCurrentAnimationDuration()
        {
            AnimationClip[] clipsInfo = _animator.runtimeAnimatorController.animationClips;
            float time=clipsInfo.First(clip=>Animator.StringToHash(clip.name)==_currentAnimationHash).length;
            return time;
        }
    }
}