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
    
    [SerializeField] private string m_name = "";
    [SerializeField] private HashSet<InputEventBase> m_inputEvents = new HashSet<InputEventBase>();
}
