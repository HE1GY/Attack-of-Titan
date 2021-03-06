using TitanFolder.States;

namespace TitanFolder.Body
{
    public class AttackScanner
    {
        public AttackScanner(BodyTrigger legsScanner, BodyTrigger handsScanner, BodyTrigger shoulderScanner,
            StateMachine stateMachine)
        {
            legsScanner.EnemyNear += _ => stateMachine.EnterState<AttackState>();
            handsScanner.EnemyNear += stateMachine.EnterState<AttackState>;
            shoulderScanner.EnemyNear += _ => stateMachine.EnterState<AttackState>(null);
        }
    }
}