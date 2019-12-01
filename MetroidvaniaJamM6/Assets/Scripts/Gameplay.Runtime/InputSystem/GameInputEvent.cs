using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "InputSystem/New game input event", fileName = "GameInputEvent")]
public class GameInputEvent : SerializedScriptableObject
{
    public string Name => m_name;
    
    public virtual bool Evaluate()
    {
        bool ret = false;
        m_inputEvents.ForEach(x => ret |= x.Evaluate());
        return ret;
    }

    public override string ToString()
    {
        return $"GameInputEvent: {m_name}";
    }

    public override int GetHashCode()
    {
        int hash = base.GetHashCode();

        hash = hash * 23 + m_name.GetHashCode();
        hash = hash * 23 + m_inputEvents.Count.GetHashCode();
        
        foreach (InputEventBase inputEvent in m_inputEvents)
        {
            hash = hash * 23 + inputEvent.GetHashCode();
        }
        
        return hash;
    }

    [SerializeField] private string m_name = "";
    [SerializeField] private HashSet<InputEventBase> m_inputEvents = new HashSet<InputEventBase>();
}

