using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace VS_Extension_Test {
    [Guid("40179140-fd2a-4e85-bdde-fe53e19b3914")]
    public class ToolWindow1 : ToolWindowPane {
        public ToolWindow1() : base(null) {
            this.Caption = "ToolWindow1";
            this.Content = new ToolWindow1Control();
        }
    }
}
