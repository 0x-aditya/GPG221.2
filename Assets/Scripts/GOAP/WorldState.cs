using System.Collections.Generic;

namespace GOAP
{
    public class WorldState
    {
        public List<Effect> effects = new List<Effect>();

        public bool HasState(string name)
        {
            return effects.Exists(e => e.name == name);
        }

        public void AddState(string name)
        {
            if (!HasState(name))
                effects.Add(new Effect(name));
        }

        public void RemoveState(string name)
        {
            effects.RemoveAll(e => e.name == name);
        }
    }
}