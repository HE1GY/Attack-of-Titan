using UnityEngine;

namespace TitanFolder
{
    public class TitanAnimationHash
    {
        public int SpleepingHash{ get; }  = Animator.StringToHash("Sleeping");
        public int WalkHash { get; } = Animator.StringToHash("Walk");
        public int LegsAttackHash { get; } = Animator.StringToHash("LegsAttack");
        public int HandsAttackHash { get; } = Animator.StringToHash("HandsAttack");
        public int SteamAttackHash { get; } =  Animator.StringToHash("SteamAttack");
    }
}