using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<IGameEventListener> listeners = new();
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }
    public void RegisterListener(IGameEventListener listener)
    {
        listeners.Add(listener);
    }
    public void UnregisterListener(IGameEventListener listener)
    {
        if(listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
public interface IGameEventListener
{
    public void OnEventRaised();
}