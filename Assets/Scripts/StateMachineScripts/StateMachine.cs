using GathererScripts;
using Utils.Events;

namespace StateMachineScripts
{
    public class StateMachine
    {
        public State IdleState{ get; private set; }
        public State SearchingForBox{ get; private set; }
        public State MovingTowardsBox{ get; private set; }
        public State PickingUpBox{ get; private set; }
        public State MovingTowardsContainer{ get; private set; }
        public State DroppingBoxState{ get; private set; }
        
        private State _currentState;
        
        public void Initialize(GathererAI gathererAI)
        {
            EventManager.OnGathererActiveStatusChanged.OnEventRaised += ChangeGatheringState;
            
            IdleState = new IdleState(gathererAI, this);
            SearchingForBox = new SearchingForBoxState(gathererAI, this);
            MovingTowardsBox = new MovingTowardsBoxState(gathererAI, this);
            PickingUpBox = new PickingUpBoxState(gathererAI, this);
            MovingTowardsContainer = new MovingTowardsContainerState(gathererAI, this);
            DroppingBoxState = new DroppingBoxState(gathererAI, this);
            
            _currentState = IdleState;
            _currentState.Enter();
        }

        public void ChangeState(State newState)
        {
            _currentState.Exit();
            _currentState = newState;
            newState.Enter();
        }

        private void ChangeGatheringState(bool state)
        {
            if (state) 
                ChangeState(SearchingForBox);
            else
                ChangeState(IdleState);
        }
    }
}
