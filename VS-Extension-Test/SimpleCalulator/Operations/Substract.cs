﻿using System.ComponentModel.Composition;

namespace VS_Extension_Test.SimpleCalulator.Operations {
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    internal class Substract : IOperation {
        public int Operate(int left, int right) {
            return left - right;
        }
    }
}
