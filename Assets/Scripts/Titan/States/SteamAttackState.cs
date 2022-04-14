using UnityEngine;

namespace Titan.States
{
    public class SteamAttackState:IState
    {
        private TitanAnimation _titanAnimation;
        private ParticleSystem _steamParticle;

        public SteamAttackState(TitanAnimation titanAnimation, ParticleSystem steamParticle)
        {
            _titanAnimation = titanAnimation;
            _steamParticle = steamParticle;
        }

        public void Enter()
        {
            _titanAnimation.PlaySteamAttack();
            _steamParticle.Play();
        }

        public void UpdateState()
        {
            
        }

        public void Exite()
        {
           
        }
    }
}