namespace StateMachineScripts
{
    public class PickingUpBoxState : State
    {
        public PickingUpBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            _gathererAI.PickUpBox();
        }
    }
}