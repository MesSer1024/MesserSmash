using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash.GUI {
    class DebugGuiOverlay {
        private List<ShortcutButton> _debugButtons;
        private Rectangle _bounds;

        public DebugGuiOverlay(Rectangle bounds) {
            _bounds = bounds;
            //debug buttons
            addDebugButton("[W-A-S-D] Movements");
            addDebugButton("[LMB-RMB] Fire weapons");
            addDebugButton("[Ctrl] speed booster!");
            addDebugButton("-----");
            addDebugButton("[G] Create 10 Random");
            addDebugButton("[P_holdable] Create 5 Random");
            addDebugButton("[R] Create 5 Ranged");
            addDebugButton("[H] Drop Health pack");
            addDebugButton("[M] Drop Money");
            addDebugButton("[Del] Force GC & Output");

            addDebugButton("[Tab] Reload Database-file");
            addDebugButton("[F1] Start Level1");
            addDebugButton("[F2] Start Level2");
            addDebugButton("[F3] Start Level3");
            addDebugButton("[F4] Start Level4");
            addDebugButton("[F5] Start Level5");
            addDebugButton("[F10] Start Special Level");
        }

        internal void addDebugButton(string text) {
            if (_debugButtons == null) {
                _debugButtons = new List<ShortcutButton>();
            }

            int row = _debugButtons.Count / 10;
            var y = _bounds.Top + (_debugButtons.Count % 10 * 33);
            var x = _bounds.Right + 20 + (row * 230);

            var button = new ShortcutButton(new Rectangle(x, y, 230, 30));
            button.setText(text);
            _debugButtons.Add(button);
        }

        public void draw(SpriteBatch sb) {
            foreach (var button in _debugButtons) {
                button.draw(sb);
            }
        }
    }
}
