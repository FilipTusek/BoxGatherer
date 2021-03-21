using GathererScripts;
using UnityEngine;
using Utils.Events;

namespace StateMachineScripts
{
    public class MovingTowardsBoxState : State
    {
        public MovingTowardsBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            _gathererAI.GathererMovement.GoTo(_gathererAI.TargetBox.transform.position);
            EventManager.OnGathererTargetReached.OnEventRaised += GoToNextState;
        }

        public override void Exit()
        {
            base.Exit();
            EventManager.OnGathererTargetReached.OnEventRaised -= GoToNextState;
        }

        private void GoToNextState()
        {
            _stateMachine.ChangeState(_stateMachine.PickingUpBox);
        }
    }
}