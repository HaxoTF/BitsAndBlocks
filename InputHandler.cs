using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BitsAndBlocks;

public class InputHandler
{
    KeyboardState prev_keystate = default;
    KeyboardState curr_keystate = default;

    MouseState prev_mouse = default;
    MouseState curr_mouse = default;

    public int scroll_delta;

    // Should be called every frame
    public void UpdateState() {

        // Input States
        prev_keystate = curr_keystate;
        curr_keystate = Keyboard.GetState();
        prev_mouse = curr_mouse;
        curr_mouse = Mouse.GetState();
        
        // Update rest
        scroll_delta = curr_mouse.ScrollWheelValue - prev_mouse.ScrollWheelValue;
    }

    // --- KEYBOARD

    // Detect if keys were just pressed or released at the exact moment
    public bool KeyJustDown(Keys _key) {
        return curr_keystate.IsKeyDown(_key) && prev_keystate.IsKeyUp(_key);
    }
    public bool KeyJustUp(Keys _key) {
        return curr_keystate.IsKeyUp(_key) && prev_keystate.IsKeyDown(_key);
    }

    // Passes the native functions
    public bool KeyDown(Keys _key) { return curr_keystate.IsKeyDown(_key); }
    public bool KeyUp(Keys _key) { return curr_keystate.IsKeyUp(_key); }

    // --- MOUSE
    
    // Mouse position
    public int GetMouseX() { return curr_mouse.X; }
    public int GetMouseY() { return prev_mouse.Y; }
    public Vector2 GetMousePos() { return new Vector2(curr_mouse.X, curr_mouse.Y); }

    // Left Mouse button states checks
    public bool LeftJustDown() {
        return curr_mouse.LeftButton == ButtonState.Pressed && prev_mouse.LeftButton == ButtonState.Released;
    }
    public bool LeftDown() {
        return curr_mouse.LeftButton == ButtonState.Pressed;
    }
    public bool LeftJustUp() {
        return curr_mouse.LeftButton == ButtonState.Released && prev_mouse.LeftButton == ButtonState.Pressed;
    }
    public bool LeftUp() {
        return curr_mouse.LeftButton == ButtonState.Released;
    }

    // Same as above but right mouse buttons
    public bool RightJustDown() {
        return curr_mouse.RightButton == ButtonState.Pressed && prev_mouse.RightButton == ButtonState.Released;
    }
    public bool RightDown() {
        return curr_mouse.RightButton == ButtonState.Pressed;
    }
    public bool RightJustUp() {
        return curr_mouse.RightButton == ButtonState.Released && prev_mouse.RightButton == ButtonState.Pressed;
    }
    public bool RightUp() {
        return curr_mouse.RightButton == ButtonState.Released;
    }
}