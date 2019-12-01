using UnityEngine;

public class AxisInputEvent
{
    public virtual float GetAxis()
    {
        return m_axisType == AxisType.Normalized ? Input.GetAxis(m_axisName) : Input.GetAxisRaw(m_axisName);
    }

    [SerializeField] protected string m_axisName = "";
    [SerializeField] protected AxisType m_axisType = AxisType.Normalized;
    
    public enum AxisType
    {
        Normalized,
        Raw
    }
}

