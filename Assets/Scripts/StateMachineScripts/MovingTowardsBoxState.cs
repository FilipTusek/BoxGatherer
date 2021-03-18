using UnityEngine;

namespace StateMachineScripts
{
    public class MovingTowardsBoxState : State
    {
        public MovingTowardsBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            _gathererAI.GathererMovement.GoTo(_gathererAI.TargetBox.transform.position);
        }

        public override void Exit()
        {
            base.Exit();
            _stateMachine.ChangeState(_gathererAI.PickingUpBox);
        }
    }
}