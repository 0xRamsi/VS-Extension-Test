using EnvDTE;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Linq;
using VS_Extension_Test.SimpleCalulator.Operations;
using static VS_Extension_Test.SimpleCalulator.Operations.OperationsDelegate;
using System.Runtime.InteropServices;

namespace VS_Extension_Test.SimpleCalulator {
    internal class SimpleCalculator {

        public struct CppSimpleCalculator {
            [DllImport("C:\\Users\\ramzi\\dev\\QT\\HalloDLLWelt\\x64\\Debug\\HalloDLLWelt.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern int SimpleCalculator(int left, int right, MyOperation operation);
        }


        private CompositionContainer _container;
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> operations;

        public SimpleCalculator() {
            // Load only when needed. Meight include other DLLs.
            try {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new AssemblyCatalog(typeof(SimpleCalculator).Assembly));

                _container = new CompositionContainer(catalog);
                _container.SatisfyImportsOnce(this);
            } catch (CompositionException exception) {
                Console.WriteLine(exception.ToString());
            }
        }


        public string Calculate(string input) {
            int left, right;
            char operation;
            int fn = FindFirstNonDigit(input);
            if (fn < 0) {
                return "Could not parse the command.";
            }

            try {
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            } catch {
                return "Could not parse the command.";
            }

            operation = input[fn];

            if(operations == null || operations.Count() == 0) {
                return "No operations registered.";
            }

            foreach (Lazy<IOperation, IOperationData> o in operations) {
                if (o.Metadata.Symbol.Equals(operation)) {
                    int res = CppSimpleCalculator.SimpleCalculator(left, right, o.Value.Operate);
                    return res.ToString();
                }
            }

            return "Operation not found.";
        }

        private int FindFirstNonDigit(string input) {
            for (int i = 0; i < input.Length; i++) {
                if (!char.IsDigit(input[i])) {
                    return i;
                }
            }
            return -1;
        }
    }
}
