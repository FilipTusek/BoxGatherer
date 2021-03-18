namespace StateMachineScripts
{
    public class DroppingBoxState : State
    {
        public DroppingBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            _gathererAI.DropTheBox();
        }
    }
}