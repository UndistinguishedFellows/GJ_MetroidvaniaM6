using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    public static InputProcessor Instance
    {
        get
        {
            if (s_instance == null && !s_quitting)
            {
                SetInstance();
            }

            return s_instance;
        }
    }
    
    public static void AddInputReceiver(GameInputEvent gameInputEvent, IInputReceiver inputReceiver)
    {
        if (!Instance.m_gameInputsCallbacks.ContainsKey(gameInputEvent))
        {
            Instance.m_gameInputsCallbacks[gameInputEvent] = new List<IInputReceiver>();
        }
        
        Instance.m_gameInputsCallbacks[gameInputEvent].Add(inputReceiver);
    }

    public static void RemoveInputEventCallback(GameInputEvent gameInput, IInputReceiver inputReceiver)
    {
        if (!Instance.m_gameInputsCallbacks.ContainsKey(gameInput))
        {
            Debug.LogWarning("Trying to remove event that has no Input Receiver");
            return;
        }
        
        Instance.m_gameInputsCallbacks[gameInput].Remove(inputReceiver);
    }
    
    public static void AddAxisReceiver(AxisInputEvent axisInputEvent, IInputReceiver inputReceiver)
    {
        if (!Instance.m_axisCallbacks.ContainsKey(axisInputEvent))
        {
            Instance.m_axisCallbacks[axisInputEvent] = new List<IInputReceiver>();
        }
        
        Instance.m_axisCallbacks[axisInputEvent].Add(inputReceiver);
    }

    public static void RemoveAxisEventCallback(AxisInputEvent axisInputEvent, IInputReceiver inputReceiver)
    {
        if (!Instance.m_axisCallbacks.ContainsKey(axisInputEvent))
        {
            Debug.LogWarning("Trying to remove event that has no Input Receiver");
            return;
        }
        
        Instance.m_axisCallbacks[axisInputEvent].Remove(inputReceiver);
    }

    // --------------------------------------------------------
    
    private void Update()
    {
        m_gameInputsCallbacks.ForEach(bind =>
        {
            if (bind.Key.Evaluate())
            {
                bind.Value?.ForEach(x => x?.OnInputEvent(bind.Key));
            }

            var axis = bind.Key.GetAxis();
            if (axis.Count > 0)
            {
                bind.Value?.ForEach(x => x?.OnAxis(axis));
            }
        });
    }

    private void OnApplicationQuit()
    {
        s_quitting = true;
    }
    
    // --------------------------------------------------------

    private static void SetInstance()
    {
        s_instance = FindObjectOfType<InputProcessor>();
        if (s_instance == null)
        {
            GameObject support = new GameObject("Input_Processor");
            s_instance = support.AddComponent<InputProcessor>();
            support.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(support);
        }

        if (s_instance == null)
        {
            throw new Exception("Could not create Input processor");
        }
    }

    // --------------------------------------------------------
    
    private static InputProcessor s_instance = null;
    private static bool s_quitting = false;
    
    private Dictionary<GameInputEvent, List<IInputReceiver>> m_gameInputsCallbacks = new Dictionary<GameInputEvent, List<IInputReceiver>>();
    private Dictionary<AxisInputEvent, List<IInputReceiver>> m_axisCallbacks = new Dictionary<AxisInputEvent, List<IInputReceiver>>();
}

