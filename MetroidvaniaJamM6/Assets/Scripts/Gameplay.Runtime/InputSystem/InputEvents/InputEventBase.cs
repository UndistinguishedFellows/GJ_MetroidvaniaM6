using System;

public abstract class InputEventBase
{
    public abstract bool Evaluate();
    
    [Serializable, Flags]
    public enum InputMode
    {
        Down = 1 << 0,
        Hold = 1 << 1,
        Up = 1 << 2
    }
}
