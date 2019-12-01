using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class InputMonoReceiver : SerializedMonoBehaviour, IInputReceiver
{
    public virtual Dictionary<GameInputEvent, UnityEvent> InputEventsCallbacks => m_inputEventsCallbacks;
    public virtual Dictionary<AxisInputEvent, UnityEvent<float>> AxisEventsCallbacks => m_axisEventsCallbacks;

    public virtual void OnInputEvent(GameInputEvent gameInputEvent)
    {
        if (!InputEventsCallbacks.ContainsKey(gameInputEvent))
        {
            Debug.LogWarning("Input is subscribed to this receiver but has no callback.");
            return;
        }
        
        InputEventsCallbacks[gameInputEvent].Invoke();
    }

    public void OnAxis(Dictionary<AxisInputEvent, float> axis)
    {
        foreach (var ax in axis)
        {
            if (!AxisEventsCallbacks.ContainsKey(ax.Key))
            {
                Debug.LogWarning("Axis is subscribed to this receiver but has no callback.");
                continue;
            }
            
            AxisEventsCallbacks[ax.Key].Invoke(ax.Value);
        }
    }
    
    protected void OnEnable()
    {
        foreach (var itr in InputEventsCallbacks)
        {
            InputProcessor.AddInputReceiver(itr.Key, this);
        }
        
        foreach (var itr in AxisEventsCallbacks)
        {
            InputProcessor.AddAxisReceiver(itr.Key, this);
        }
    }
    
    protected void OnDisable()
    {
        foreach (var itr in InputEventsCallbacks)
        {
            InputProcessor.RemoveInputEventCallback(itr.Key, this);
        }
        
        foreach (var itr in AxisEventsCallbacks)
        {
            InputProcessor.RemoveAxisEventCallback(itr.Key, this);
        }
    }
    
    [SerializeField] protected Dictionary<GameInputEvent, UnityEvent> m_inputEventsCallbacks = new Dictionary<GameInputEvent, UnityEvent>();
    [SerializeField] protected Dictionary<AxisInputEvent, UnityEvent<float>> m_axisEventsCallbacks = new Dictionary<AxisInputEvent, UnityEvent<float>>();
}
