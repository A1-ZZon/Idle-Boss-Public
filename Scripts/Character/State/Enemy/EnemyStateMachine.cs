public class EnemyStateMachine : CharacterStateMachine
{
    public EnemyIdleState IdleState {  get; private set; }
    public EnemyMoveState MoveState {  get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyBuffState BuffState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        Character = enemy;
        Data = enemy.Data;
        Stat = enemy.Stat;

        IdleState = new EnemyIdleState(this);
        MoveState = new EnemyMoveState(this);
        AttackState = new EnemyAttackState(this);
        BuffState = new EnemyBuffState(this);
    }
}

