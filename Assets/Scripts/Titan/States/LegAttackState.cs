namespace Titan.States
{
    public class LegAttackState:IState
    {
        private TitanAnimation _titanAnimation;

        public LegAttackState(TitanAnimation titanAnimation)
        {
            _titanAnimation = titanAnimation;
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