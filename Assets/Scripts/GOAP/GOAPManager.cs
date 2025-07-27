using System.Collections.Generic;

namespace GOAP
{
    public class GOAPManager
    {
        private WorldState worldState;
        private List<Action> actions;

        public GOAPManager(WorldState worldState)
        {
            this.worldState = worldState;
            actions = new List<Action>();
        }

        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        public Action GetNextAction(string desiredState)
        {
            foreach (var action in actions)
            {
                foreach (var effect in action.effects)
                {
                    if (effect.name == desiredState && action.CanExecute())
                        return action;
                }
            }
            return null;
        }
    }
}