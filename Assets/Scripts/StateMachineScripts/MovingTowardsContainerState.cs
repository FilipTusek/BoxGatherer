namespace StateMachineScripts
{
    public class MovingTowardsContainerState : State
    {
        public MovingTowardsContainerState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}