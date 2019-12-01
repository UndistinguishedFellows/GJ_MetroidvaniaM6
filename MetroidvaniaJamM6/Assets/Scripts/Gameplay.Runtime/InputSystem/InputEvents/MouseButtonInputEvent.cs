using Sirenix.OdinInspector;
using UnityEngine;

public class MouseButtonInputEvent : InputEventBase
{
    public override bool Evaluate()
    {
        bool button = false;
        
        if (m_mode.HasFlag(InputMode.Down))
            button |= Input.GetMouseButtonDown(m_buttonCode);
        if (m_mode.HasFlag(InputMode.Hold))
            button |= Input.GetMouseButton(m_buttonCode);
        if (m_mode.HasFlag(InputMode.Up))
            button |= Input.GetMouseButtonDown(m_buttonCode);
        
        return button;
    }
    
    public override int GetHashCode()
    {
        int hash = 17;

        hash = hash * 23 + m_buttonCode.GetHashCode();
        hash = hash * 23 + m_mode.GetHashCode();

        return hash;
    }
    
    public override string ToString()
    {
        return $"Mouse button input event: {m_buttonCode} - Mode: {m_mode}";
    }

    [SerializeField][ValueDropdown(nameof(s_mouseButtons))] protected int m_buttonCode = 0; 
    [SerializeField] protected InputMode m_mode = InputMode.Up;
    
    private static ValueDropdownList<int> s_mouseButtons = new ValueDropdownList<int>()
    {
        {"Left", 0},
        {"Right", 1},
        {"Middle", 2}
    };
}
