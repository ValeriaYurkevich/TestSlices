﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EventHandler
{
    public enum Event
    {
        OnSlicePlaced,
        OnCircleCompletedSuccesfully,
        OnCircleCompletedWithDifColors
    }

    private Dictionary<Event, List<Action>> Subscriptions { get; set; }

    EventHandler()
    {
        Subscriptions = new Dictionary<Event, List<Action>>();
    }

    public void SubscribeToEvent(Event eventName, Action handler)
    {
        if (!Subscriptions.ContainsKey(eventName))
        {
            Subscriptions[eventName] = new List<Action>();
        }

        Subscriptions[eventName].Add(handler);
    }

    public void Dispatch(Event eventToDispatch)
    {
        if (Subscriptions.ContainsKey(eventToDispatch))
        {
            Subscriptions[eventToDispatch].ForEach(e => e());
        }
    }

    #region Singleton

    private static EventHandler _instance;

    public static EventHandler Current
    {
        get
        {
            if (_instance == null)
                _instance = new EventHandler();

            return _instance;

        }
    }

    #endregion
}
