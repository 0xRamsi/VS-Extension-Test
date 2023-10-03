using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using VS_Extension_Test.SimpleCalulator;
using static VS_Extension_Test.SimpleCalulator.SimpleCalculator;

namespace VS_Extension_Test {
    public partial class ToolWindow1Control : UserControl {
        private const string DLLPath = "<< Here goes the path, to where you checked out the project >>\\VS-Extension-Test\\x64\\Debug\\Dll1.dll";

        struct Kernel {
            [DllImport("User32.dll", EntryPoint = "MessageBox", CharSet = CharSet.Auto)]
            internal static extern int MsgBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        }
        struct MessagePrompter {

            [DllImport(DLLPath, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern void testFunc(IntPtr instance, string phrase);
            [DllImport(DLLPath, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr CreateInstance(int i);
            [DllImport(DLLPath, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern void ReleaseInstance(IntPtr instance);
        }
        struct Timer {
            [DllImport(DLLPath, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern void setTimerout(IntPtr timer, int delay, IntPtr callback);
            [DllImport(DLLPath, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr CreateTimer();
            [DllImport(DLLPath, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern void ReleaseTimer(IntPtr instance);
        }

        private TimeSpan _time;
        private DispatcherTimer _timer;
        IntPtr hallo;
        IntPtr timer;
        private SimpleCalculator _calculator;

        public ToolWindow1Control() {
            this.InitializeComponent();
            _time = TimeSpan.FromMinutes(20);

            _timer = new DispatcherTimer(new TimeSpan(0,0,1), DispatcherPriority.Background, delegate {
                lblTimer.Content = _time.ToString("c");
                if (_time == TimeSpan.Zero) {
                    _timer.Stop();
                    SystemSounds.Beep.Play();
                    btnStartStop.Content = "Start";
                    _time = TimeSpan.FromMinutes(20);
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.IsEnabled = false;

            hallo = MessagePrompter.CreateInstance(42);
            //timer = Timer.CreateTimer();
            _calculator = new SimpleCalculator();
        }

        ~ToolWindow1Control() {
            MessagePrompter.ReleaseInstance(hallo);
            //Timer.ReleaseTimer(timer);
        }
        private void btnStartStop_Click(object sender, RoutedEventArgs e) {
            if (_timer.IsEnabled) {
                _timer.Stop();
                btnStartStop.Content = "Start";
            } else {
                _timer.Start();
                btnStartStop.Content = "Stop";
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e) {
            _time = TimeSpan.FromMinutes(20);
            lblTimer.Content = _time.ToString("c");
            _timer.Stop();
            btnStartStop.Content = "Start";
        }

        private void btnCpp1_Click(object sender, RoutedEventArgs e) {
            MessagePrompter.testFunc(hallo, "C# Data is here...");
        }

        private static int Adder(int a, int b) { return a + b; }

        private void btnCpp2_Click(object sender, RoutedEventArgs e) {
            if (
            int.TryParse(leftOperand.Text, out int left) &&
            int.TryParse(rightOperand.Text, out int right)
                ) {
                int res = CppSimpleCalculator.SimpleCalculator(left, right, Adder);
                AdderResult.Content = res.ToString();
            } else {
                MessageBox.Show("Please enter numbers.");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCpp3_Click(object sender, RoutedEventArgs e) {
            CalculationResult.Content = _calculator.Calculate(txtExpression.Text);
        }
    }
}