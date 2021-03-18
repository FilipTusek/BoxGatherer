using UnityEngine;

namespace StateMachineScripts
{
    public class SearchingForBoxState : State
    {
        public SearchingForBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            _gathererAI.SetTargetBox();
            _gathererAI.SetTargetContainer();
        }
    }
}
