namespace StateMachineScripts
{
    public class PickingUpBoxState : State
    {
        public PickingUpBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}