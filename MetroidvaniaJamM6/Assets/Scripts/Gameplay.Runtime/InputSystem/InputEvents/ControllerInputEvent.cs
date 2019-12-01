using Sirenix.OdinInspector;
using UnityEngine;

public class ControllerInputEvent : InputEventBase
{
    public override bool Evaluate()
    {
        bool button = false;
        
        if (m_mode.HasFlag(InputMode.Down))
            button |= Input.GetButtonDown(m_buttonName);
        if (m_mode.HasFlag(InputMode.Hold))
            button |= Input.GetButton(m_buttonName);
        if (m_mode.HasFlag(InputMode.Up))
            button |= Input.GetButtonUp(m_buttonName);
        
        

        return button;
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
