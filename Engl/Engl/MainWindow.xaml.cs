using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Engl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += Window_SourceInitialized;
            this.Loaded += Window_Loaded;
            this.StateChanged += Window_StateChanged;
            this.Closing += Window_Closing;
            ShowInTaskbar = false;
            textBlockShoot1.PreviewMouseDown += TextBlockShoot_PreviewMouseDown;
            textBlockShoot2.PreviewMouseDown += TextBlockShoot_PreviewMouseDown;
        }

        private void TextBlockShoot_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var animation = new DoubleAnimation();
            if (imageGrid.Height <= 0)
            {
                animation.From = 0;
                animation.To = 294;
            }
            else
            {
                animation.From = 294;
                animation.To = 0;
            }
            animation.Duration = TimeSpan.FromSeconds(1.5);
            imageGrid.BeginAnimation(HeightProperty, animation);
        }

        private void Shoot()
        {
            if (imageGrid.Height <= 0)
            {
                do
                {
                    imageGrid.Height++;
                    Thread.Sleep(10);
                } while (imageGrid.Height < 372);
            }
            else
            {
                do
                {
                    imageGrid.Height--;
                    Thread.Sleep(10);
                } while (imageGrid.Height > 0);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 100;
            this.Top = desktopWorkingArea.Bottom /2.0 - this.Height /2.0;
            ShowDesktop.AddHook(this);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);

            /*
            SetWindowLong(helper.Handle,
                          GWL_EXSTYLE,
                          GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);*/
        }

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_SYSCOMMAND:
                    int command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
