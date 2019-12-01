using Sirenix.OdinInspector;
using UnityEngine;

public class ControllerInputEvent : InputEventBase
{
    public override bool Evaluate()
    {
        bool button = false;
        
        if (m_mode.HasFlag(InputMode.Down))
            button |= Input.GetButtonDown(ButtonName);
        if (m_mode.HasFlag(InputMode.Hold))
            button |= Input.GetButton(ButtonName);
        if (m_mode.HasFlag(InputMode.Up))
            button |= Input.GetButtonUp(ButtonName);
        
        return button;
    }
    
    public override int GetHashCode()
    {
        int hash = 17;

        hash = hash * 23 + ButtonName.GetHashCode();
        hash = hash * 23 + CustomButtonName.GetHashCode();
        hash = hash * 23 + m_mode.GetHashCode();

        return hash;
    }

    public override string ToString()
    {
        return $"Controller input event: {ButtonName}(Custom: {CustomButtonName}) - Mode: {m_mode}";
    }

    protected virtual bool CustomButtonName => m_buttonName == s_controllerButtons[0].Value;
    protected virtual string ButtonName => CustomButtonName ? m_customButtonName : m_buttonName;

    [SerializeField] [ValueDropdown(nameof(s_controllerButtons))] protected string m_buttonName = "";
    [SerializeField] [ShowIf("@CustomButtonName", true)] protected string m_customButtonName = "";
    [SerializeField] protected InputMode m_mode = InputMode.Up;
    
    private static ValueDropdownList<string> s_controllerButtons = new ValueDropdownList<string>()
    {
        {"Custom", "Custom"},
        {"Joystick 0", "joystick button 0"},
        {"Joystick 1", "joystick button 1"},
        {"Joystick 2", "joystick button 2"}
    };
}
