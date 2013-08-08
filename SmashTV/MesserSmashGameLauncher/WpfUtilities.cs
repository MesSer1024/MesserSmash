using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MesserSmashGameLauncher {
    static class WpfUtilities {
        public static void cloneControlProperties(UserControl control, UserControl template) {
            control.HorizontalAlignment = template.HorizontalAlignment;
            control.VerticalAlignment = template.VerticalAlignment;
            control.Margin = template.Margin;
        }
    }
}
