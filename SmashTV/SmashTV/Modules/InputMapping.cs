using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.IO;

namespace MesserSmash.Modules
{
    public enum InputAction
    {
        NULL,
        Sprint,
        NavigateUp,
        NavigateDown,
        NavigateRight,
        NavigateLeft,
    }

    static class InputMapping
    {
        private const Keys NULL_KEY = Keys.F24;
        private const string INPUT_SETTINGS_FILE = "./input.cfg";

        private static Dictionary<InputAction, Keys> _keyboardBinding;

        static InputMapping()
        {   
            _keyboardBinding = new Dictionary<InputAction, Keys>();
            _keyboardBinding.Add(InputAction.Sprint, Keys.LeftControl);
            _keyboardBinding.Add(InputAction.NavigateUp, Keys.W);
            _keyboardBinding.Add(InputAction.NavigateDown, Keys.S);
            _keyboardBinding.Add(InputAction.NavigateLeft, Keys.A);
            _keyboardBinding.Add(InputAction.NavigateRight, Keys.D);
        }
        
        private class InputData
        {
            public InputData()
            {
                AllKeyboardMappings = new Dictionary<string, int>();
                KeyBindings = new Dictionary<InputAction, Keys>();
            }
            public Dictionary<string, int> AllKeyboardMappings { get; set; }
            public Dictionary<InputAction, Keys> KeyBindings { get; set; }
        }

        public static void saveLayout()
        {
            var names = Enum.GetNames(typeof(Keys)).Cast<string>().ToList();
            var keys = Enum.GetValues(typeof(Keys)).Cast<int>().ToList();

            var data = new InputData();
            for (int i = 0; i < names.Count; ++i)
            {
                data.AllKeyboardMappings.Add(names[i], keys[i]);
                data.KeyBindings = _keyboardBinding;
            }

            var blob = JsonConvert.SerializeObject(data);
            using (var sw = new StreamWriter(INPUT_SETTINGS_FILE, false))
            {
                sw.Write(blob);
                sw.Flush();
                sw.Close();
            }
        }

        public static void loadLayout()
        {
            var fi = new FileInfo(INPUT_SETTINGS_FILE);
            if(!fi.Exists) 
                throw new Exception();
            
            var blob = File.ReadAllText(fi.FullName);
            var data = JsonConvert.DeserializeObject<InputData>(blob);
            _keyboardBinding = data.KeyBindings;
        }

        //static void rebind(InputAction action, Keys key)
        //{
        //    asdfasdf //#TODO: do the rebind-functionality somehow, GUI-screen? settings.ini?


        //    //reset any multiple keys
        //    InputAction foo = InputAction.NULL;
        //    foreach (var pair in _keyboardBinding)
        //    {
        //        if (pair.Value == key)
        //        {
        //            foo = pair.Key;
        //        }
        //    }
        //    //rebind
        //    _keyboardBinding[foo] = NULL_KEY;
        //    _keyboardBinding[action] = key;
        //}

        internal static bool isKeyDown(InputAction inputAction)
        {
            return Utils.isKeyDown(_keyboardBinding[inputAction]);
        }

        internal static bool isKeyUp(InputAction inputAction)
        {
            return Utils.isKeyUp(_keyboardBinding[inputAction]);
        }

        internal static bool isNewPress(InputAction inputAction)
        {
            return Utils.isNewKeyPress(_keyboardBinding[inputAction]);
        }
    }
}
