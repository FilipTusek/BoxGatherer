using UnityEngine;
using UnityEngine.UIElements;

namespace StateMachineScripts
{
    public class MovingTowardsContainerState : State
    {
        public MovingTowardsContainerState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            var container = _gathererAI.TargetContainer;
            var containerPosition = container.position;
            var targetPosition = containerPosition - (containerPosition - _gathererAI.transform.position).normalized.x * new Vector3(container.localScale.x / 2f, 0, 0);
            _gathererAI.GathererMovement.GoTo(targetPosition);
        }

        public override void Exit()
        {
            base.Exit();
            _stateMachine.ChangeState(_gathererAI.DroppingBoxState);
        }
    }
}