using System;
using System.Threading;

namespace PuppeteerAot.States
{
    public class StateManager
    {
        private State _currentState;

        public StateManager()
        {
            Initial = new InitialState(this);
            Starting = new ProcessStartingState(this);
            Started = new StartedState(this);
            Exiting = new ExitingState(this);
            Killing = new KillingState(this);
            Exited = new ExitedState(this);
            Disposed = new DisposedState(this);
            CurrentState = Initial;
        }

        public State CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        public State Initial { get; set; }

        public State Starting { get; set; }

        public StartedState Started { get; set; }

        public State Exiting { get; set; }

        public State Killing { get; set; }

        public ExitedState Exited { get; set; }

        public State Disposed { get; set; }

        public bool TryEnter(LauncherBase p, State fromState, State toState)
        {
            if (Interlocked.CompareExchange(ref _currentState, toState, fromState) == fromState)
            {
                fromState.Leave(p);
                return true;
            }

            return false;
        }
    }
}
