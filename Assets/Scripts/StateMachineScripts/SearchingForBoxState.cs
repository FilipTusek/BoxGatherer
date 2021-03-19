using UnityEngine;

namespace StateMachineScripts
{
    public class SearchingForBoxState : State
    {
        public SearchingForBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            if (_gathererAI.IsCarryingBox) {
                _stateMachine.ChangeState(_gathererAI.MovingTowardsContainer);
                return;
            }

            _gathererAI.SetTargetBox();
            _gathererAI.SetTargetContainer();
        }
    }
}
