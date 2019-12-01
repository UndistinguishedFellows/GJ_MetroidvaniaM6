using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "InputSystem/New game input event", fileName = "GameInputEvent")]
public class GameInputEvent : SerializedScriptableObject
{
    public virtual bool Evaluate()
    {
        bool ret = false;
        m_inputEvents.ForEach(x => ret |= x.Evaluate());
        return ret;
    }

    public virtual Dictionary<AxisInputEvent, float> GetAxis()
    {
        Dictionary<AxisInputEvent, float> ret = new Dictionary<AxisInputEvent, float>(); // TODO: Could cache a dictionary instance to avoid GC but probably is really minor
        m_axisInputEvents.ForEach(x => ret.Add(x, x.GetAxis()));
        return ret;
    }
    
    [SerializeField] private string m_name = "";
    [SerializeField] private HashSet<InputEventBase> m_inputEvents = new HashSet<InputEventBase>();
    [SerializeField] private HashSet<AxisInputEvent> m_axisInputEvents = new HashSet<AxisInputEvent>();
}
