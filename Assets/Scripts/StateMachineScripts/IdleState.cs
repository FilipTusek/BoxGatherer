namespace StateMachineScripts
{
    public class IdleState : State
    {
        public IdleState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            _gathererAI.GathererMovement.Stop();
        }
    }
}