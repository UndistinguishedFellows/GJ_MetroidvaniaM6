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

    [SerializeField] protected KeyCode m_keyCode = KeyCode.None;
    [SerializeField] protected InputMode m_mode = InputMode.Up;
    [SerializeField] protected KeyCode m_modifier = KeyCode.None;
}
