﻿using System;
using System.Collections.Generic;
using DeftSharp.Windows.Input.Mouse.Interceptors;
using DeftSharp.Windows.Input.Shared.Abstraction.Mouse;
using DeftSharp.Windows.Input.Shared.Attributes;

namespace DeftSharp.Windows.Input.Mouse;

public sealed class MouseManipulator : IMouseManipulator
{
    private readonly MouseManipulatorInterceptor _mouseInterceptor;
    public IEnumerable<MouseInputEvent> LockedKeys => _mouseInterceptor.LockedKeys;

    public event Action<MouseInputEvent>? ClickPrevented;

    public MouseManipulator()
    {
        _mouseInterceptor = MouseManipulatorInterceptor.Instance;
        _mouseInterceptor.ClickPrevented += OnInterceptorClickPrevented;
    }

    public bool IsKeyLocked(MouseInputEvent mouseEvent) => _mouseInterceptor.IsKeyLocked(mouseEvent);

    [DangerousBehavior("Be careful with the use of this method. You can completely lock your mouse")]
    public void Prevent(PreventMouseOption mouseEvent) => _mouseInterceptor.Prevent(mouseEvent);

    public void Release(PreventMouseOption mouseEvent) => _mouseInterceptor.Release(mouseEvent);
    public void ReleaseAll() => _mouseInterceptor.ReleaseAll();
    public void SetPosition(int x, int y) => _mouseInterceptor.SetPosition(x, y);
    public void Click(int x, int y, MouseButton button = MouseButton.Left) => _mouseInterceptor.Click(button, x, y);
    public void Click(MouseButton button = MouseButton.Left) => _mouseInterceptor.Click(button);

    public void DoubleClick(int x, int y)
    {
        Click(x, y);
        Click(x, y);
    }

    public void DoubleClick()
    {
        Click();
        Click();
    }

    public void Dispose() => _mouseInterceptor.ClickPrevented -= OnInterceptorClickPrevented;

    private void OnInterceptorClickPrevented(MouseInputEvent mouseEvent) => ClickPrevented?.Invoke(mouseEvent);
}