
using System.Collections.Generic;

public interface IInputReceiver
{
    void OnInputEvent(GameInputEvent gameInputEvent);
    void OnAxis(Dictionary<AxisInputEvent, float> axis);
}
