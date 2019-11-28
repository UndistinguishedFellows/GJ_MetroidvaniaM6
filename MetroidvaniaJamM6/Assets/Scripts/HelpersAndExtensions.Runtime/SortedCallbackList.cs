using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SortedCallbackList<T> : Dictionary<int, List<Action<T>>>
{
    private void AddCallback(int priority, Action<T> callback)
    {
        if (callback == null)
        {
            Debug.LogError("Trying to add null callback");
            return;
        }

        if (!TryGetValue(priority, out List<Action<T>> list))
        {
            this[priority] = new List<Action<T>>();
        }
        
        this[priority].Add(callback);
        m_dirty = true;
    }

    public void RemoveCallback(int prioriy, Action<T> callback)
    {
        if (callback == null)
        {
            Debug.LogError("Trying to remove null callback.");
            return;
        }

        if (!TryGetValue(prioriy, out List<Action<T>> list))
        {
            Debug.LogError("Trying to remove callback on not initialized priority.");
            return;
        }

        this[prioriy].Remove(callback);
        m_dirty = true;
    }

    public void Invoke(T value)
    {
        if (m_dirty)
        {
            SortKeys();
            m_dirty = false;
        }

        foreach (int sortedKey in m_sortedKeys)
        {
            this[sortedKey]?.ForEach(x => x?.Invoke(value));
        }
    }

    protected void SortKeys()
    {
        m_sortedKeys = Keys.OrderBy(x => x);
    }
    
    private IOrderedEnumerable<int> m_sortedKeys = null;
    private bool m_dirty = false;
}
