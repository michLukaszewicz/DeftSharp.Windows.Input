﻿using System;
using System.Collections.Generic;
using DeftSharp.Windows.Input.Mouse;
using DeftSharp.Windows.Input.Shared.Subscriptions;

namespace DeftSharp.Windows.Input.Shared.Abstraction.Mouse;

public interface IMouseListener : IDisposable
{
    IEnumerable<MouseSubscription> Subscriptions { get; }

    bool IsListening { get; }

    Coordinates GetPosition();

    MouseSubscription Subscribe(MouseEvent mouseEvent, Action<MouseInputEvent> onAction,
        TimeSpan? intervalOfClick = null);

    MouseSubscription Subscribe(MouseEvent mouseEvent, Action onAction, TimeSpan? intervalOfClick = null);
    MouseSubscription SubscribeOnce(MouseEvent mouseEvent, Action<MouseInputEvent> onAction);
    MouseSubscription SubscribeOnce(MouseEvent mouseEvent, Action onAction);
    IEnumerable<MouseSubscription> SubscribeAll(Action<MouseInputEvent> onAction, TimeSpan? intervalOfClick = null);

    void Unsubscribe(MouseEvent mouseEvent);
    void Unsubscribe(Guid id);
    void UnsubscribeAll();
}