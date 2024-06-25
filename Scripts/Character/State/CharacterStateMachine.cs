using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CharacterStateMachine
{
    protected IState currentState;

    public CharacterSO Data;
    public Character Character;
    public Health Target;
    public CharacterStat Stat;

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
    
    public bool CheckEqualState(IState state)
    {
        return currentState == state;
    }
}