using System.Collections;
using System.Collections.Generic;

namespace GOAP
{
    public class Action
    {

        public string name;
        public WorldState worldState;
        public List<Prerequisite> prerequisites = new List<Prerequisite>();
        public List<Effect> effects = new List<Effect>();

        public Action(string name, WorldState worldState)
        {
            this.name = name;
            this.worldState = worldState;
        }

        public bool CanExecute()
        {
            foreach (var pre in prerequisites)
            {
                if (!worldState.HasState(pre.name))
                    return false;
            }
            return true;
        }

        public virtual void DoAction()
        {
        }
        
        public virtual IEnumerator DoActionWithMovement(Movement movement)
        {
            yield return null;
        }
    }
}