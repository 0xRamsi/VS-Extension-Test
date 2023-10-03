using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS_Extension_Test.SimpleCalulator.Operations {
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '%')]
    internal class Mod : IOperation {
        public int Operate(int left, int right) {
            return left % right;
        }
    }
}
