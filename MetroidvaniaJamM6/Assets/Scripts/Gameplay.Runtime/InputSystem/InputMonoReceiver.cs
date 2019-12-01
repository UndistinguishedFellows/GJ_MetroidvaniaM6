using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class InputMonoReceiver : SerializedMonoBehaviour, IInputReceiver
{
    public virtual Dictionary<GameInputEvent, UnityEvent> InputEventsCallbacks => m_inputEventsCallbacks;

    public virtual void OnInputEvent(GameInputEvent gameInputEvent)
    {
        if (!InputEventsCallbacks.ContainsKey(gameInputEvent))
        {
            Debug.LogWarning("Input is subscribed to this receiver but has no callback.");
            return;
        }
        
        InputEventsCallbacks[gameInputEvent].Invoke();
    }

    protected void OnEnable()
    {
        foreach (var itr in InputEventsCallbacks)
        {
            InputProcessor.AddInputReceiver(itr.Key, this);
        }
    }
    
    protected void OnDisable()
    {
        foreach (var itr in InputEventsCallbacks)
        {
            InputProcessor.RemoveInputEventCallback(itr.Key, this);
        }
    }
    
    [SerializeField] protected Dictionary<GameInputEvent, UnityEvent> m_inputEventsCallbacks = new Dictionary<GameInputEvent, UnityEvent>();
}
