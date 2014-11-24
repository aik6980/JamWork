using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

using SharpDX.Multimedia;
using SharpDX.RawInput;
using SharpDX.DirectInput;

using Util;

namespace Input
{
    class InputManager : Util.Singleton<InputManager>
    {
        Keyboard    m_Keyboard;
        Mouse       m_Mouse;

        MouseState      m_CurrMouseState, m_PrevMouseState;
        KeyboardState   m_CurrKBState, m_PrevKBState;

        public void Init()
        {
            var directinput = new DirectInput();
            
            m_Keyboard = new Keyboard(directinput);
            m_Mouse = new Mouse(directinput);

            m_Keyboard.Acquire();
            m_Mouse.Acquire();

            m_CurrMouseState = m_Mouse.GetCurrentState();
            m_CurrKBState = m_Keyboard.GetCurrentState();
        }

        public void Destroy()
        {
            m_Mouse.Dispose();
            m_Keyboard.Dispose();
        }

        public void Update()
        {
            m_PrevMouseState = m_CurrMouseState;
            m_CurrMouseState = m_Mouse.GetCurrentState();

            m_PrevKBState = m_CurrKBState;
            m_CurrKBState = m_Keyboard.GetCurrentState();
        }

        public Point ClientMousePosition()
        {
            return WinGame.Program.RenderForm.PointToClient(Cursor.Position);
        }

        public Point ScreenMousePosition()
        {
            return Cursor.Position;
        }

        public bool KeyDown(Key key)
        {
            return m_CurrKBState.IsPressed(key);
        }

        public bool KeyPressed(Key key)
        {
            if(m_PrevKBState.IsPressed(key) == false
                && m_CurrKBState.IsPressed(key) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}