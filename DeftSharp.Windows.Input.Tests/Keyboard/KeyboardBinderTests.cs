﻿namespace DeftSharp.Windows.Input.Tests.Keyboard;

public class KeyboardBinderTests
{
    private readonly ThreadRunner _threadRunner = new();

    [Fact]
    public async void KeyboardBinder_IsKeyBound()
    {
        var keyboardBinder = new KeyboardBinder();

        await _threadRunner.Run(() =>
        {
            keyboardBinder.Bind(Key.A, Key.B);

            Assert.True(keyboardBinder.IsKeyBounded(Key.A));
            Assert.False(keyboardBinder.IsKeyBounded(Key.B));
        });
    }
    
    [Fact]
    public async void KeyboardBinder_IsKeyBoundWhenBindingTheSameKey()
    {
        var keyboardBinder = new KeyboardBinder();

        await _threadRunner.Run(() =>
        {
            keyboardBinder.Bind(Key.C, Key.C);

            Assert.False(keyboardBinder.IsKeyBounded(Key.C));
        });
    }
    
    [Fact]
    public async void KeyboardBinder_BindMultipleKeysAtOnce()
    {
        var keyboardBinder = new KeyboardBinder();

        await _threadRunner.Run(() =>
        {
            var keys = new List<Key>
            {
                Key.W,
                Key.X,
                Key.Y,
                Key.Z
            };

            keyboardBinder.BindMany(keys, Key.A);

            Assert.True(keyboardBinder.IsKeyBounded(Key.W));
            Assert.True(keyboardBinder.IsKeyBounded(Key.X));
            Assert.True(keyboardBinder.IsKeyBounded(Key.Y));
            Assert.True(keyboardBinder.IsKeyBounded(Key.Z));
            Assert.False(keyboardBinder.IsKeyBounded(Key.A));
        });
    }
    
    [Fact]
    public async void KeyboardBinder_BindEmptyKeyCollection()
    {
        var keyboardBinder = new KeyboardBinder();

        await _threadRunner.Run(() =>
        {
            var keys = new List<Key>();

            keyboardBinder.BindMany(keys, Key.A);
            
            Assert.Empty(keyboardBinder.BoundedKeys);
        });
    }
    
    [Fact]
    public async void KeyboardBinder_Unbind()
    {
        var keyboardBinder = new KeyboardBinder();

        await _threadRunner.Run(() =>
        {
            keyboardBinder.Bind(Key.A, Key.B);
            Assert.True(keyboardBinder.IsKeyBounded(Key.A));

            keyboardBinder.Unbind(Key.A);

            Assert.Empty(keyboardBinder.BoundedKeys);
        });
    }
    
    [Fact]
    public async void KeyboardBinder_UnbindAll()
    {
        var keyboardBinder = new KeyboardBinder();

        await _threadRunner.Run(() =>
        {
            var keys = new List<Key>
            {
                Key.Cancel,
                Key.Enter,
                Key.End,
                Key.Tab
            };
            
            keyboardBinder.BindMany(keys, Key.A);
            Assert.Equal(4, keyboardBinder.BoundedKeys.Count);

            keyboardBinder.UnbindAll();

            Assert.Empty(keyboardBinder.BoundedKeys);
        });
    }
    
    [Fact]
    public async void KeyboardBinder_UnbindKeyThatHasNotBeenBound()
    {
        var keyboardBinder = new KeyboardBinder();

        await _threadRunner.Run(() =>
        {
            Assert.Empty(keyboardBinder.BoundedKeys);
                
            keyboardBinder.Unbind(Key.A);

            Assert.Empty(keyboardBinder.BoundedKeys);
        });
    }
}