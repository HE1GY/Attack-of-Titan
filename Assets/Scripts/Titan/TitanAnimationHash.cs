using UnityEngine;

namespace Titan
{
    public class TitanAnimationHash
    {
        public int SpleepingHash{ get; }  = Animator.StringToHash("Sleeping");
        public int WalkHash { get; } = Animator.StringToHash("Walk");
        public int LegAttackHash { get; } = Animator.StringToHash("LegAttack");
        public int HandAttackHash { get; } = Animator.StringToHash("HandAttack");
        public int SteamAttackHash { get; } =  Animator.StringToHash("SteamAttack");
    }
}