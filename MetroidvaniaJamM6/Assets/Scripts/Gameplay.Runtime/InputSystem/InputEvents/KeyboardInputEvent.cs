using UnityEngine;

public class KeyboardInputEvent : InputEventBase
{
    public override bool Evaluate()
    {
        bool modifier = m_modifier != KeyCode.None ? Input.GetKey(m_modifier) : true;
        bool key = false;
        
        if (m_mode.HasFlag(InputMode.Down))
            key |= Input.GetKeyDown(m_keyCode);
        if (m_mode.HasFlag(InputMode.Hold))
            key |= Input.GetKey(m_keyCode);
        if (m_mode.HasFlag(InputMode.Up))
            key |= Input.GetKeyUp(m_keyCode);

        return key && modifier;
    }
    
    public override int GetHashCode()
    {
        int hash = 17;

        hash = hash * 23 + m_keyCode.GetHashCode();
        hash = hash * 23 + m_mode.GetHashCode();
        hash = hash * 23 + m_modifier.GetHashCode();

        return hash;
    }
    
    public override string ToString()
    {
        return $"Keyboard input event: {m_keyCode} - Mode: {m_mode} - Modifier: {m_modifier}";
    }

    [SerializeField] protected KeyCode m_keyCode = KeyCode.None;
    [SerializeField] protected InputMode m_mode = InputMode.Up;
    [SerializeField] protected KeyCode m_modifier = KeyCode.None;
}
