using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviourSingleton<EventManager>
{
    private Dictionary<NameEvent, Action<object[]>> _subscribers = new();

    public void Subscribe(NameEvent eventId, Action<object[]> callback)
    {
        if (!_subscribers.ContainsKey(eventId))
            _subscribers.Add(eventId, callback);
        else
            _subscribers[eventId] += callback;
    }

    public void Unsubscribe(NameEvent eventId, Action<object[]> callback)
    {
        if (!_subscribers.ContainsKey(eventId)) return;

        _subscribers[eventId] -= callback;
    }

    public void Trigger(NameEvent eventId, params object[] parameters)
    {
        if (!_subscribers.ContainsKey(eventId))
            return;

        _subscribers[eventId]?.Invoke(parameters);
    }
}

[Serializable]
public enum NameEvent
{
    OnShopOpened,
    OnShopClosed,
    OnClotheBought,
    OnClotheSold,
    OnMoneyUpdated,
    OnInventoryOpened,
    OnInventoryClosed,
    OnClotheTypeChanged,
}