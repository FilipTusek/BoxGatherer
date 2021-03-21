using GathererScripts;
using UnityEngine;

namespace StateMachineScripts
{
    public class SearchingForBoxState : State
    {
        public SearchingForBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            SetTargetBox();
            SetTargetContainer();
        }

        private void SetTargetBox()
        {
            if (_gathererAI.IsCarryingBox) {
                _stateMachine.ChangeState(_stateMachine.MovingTowardsContainer);
                return;
            }

            var boxes = Physics2D.OverlapCircleAll(_gathererAI.transform.position, _gathererAI.DetectionRadius, _gathererAI.BoxLayerMask);
            if (boxes == null || boxes.Length == 0) {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }
        
            var closestDistance = float.MaxValue;
            Box closestBox = null;
        
            foreach (var box in boxes) {
                var distance = Vector2.Distance(_gathererAI.transform.position, box.transform.position);
                if (closestDistance > distance) {
                    closestDistance = distance;
                    closestBox = box.GetComponent<Box>();
                }
            }
            
            _gathererAI.TargetBox = closestBox;
            _stateMachine.ChangeState(_stateMachine.MovingTowardsBox);
        }

        private void SetTargetContainer()
        {
            if (_gathererAI.TargetBox == null) return;
            _gathererAI.TargetContainer = _gathererAI.TargetBox.TypeOfBox == Box.BoxType.Blue ? _gathererAI.BlueContainer : _gathererAI.RedContainer;
        }
    }
}
