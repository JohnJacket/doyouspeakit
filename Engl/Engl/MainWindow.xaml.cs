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

        enum eMoveDirection
        {
            Up,
            Down
        }

        enum eLang
        {
            eng,
            ukr
        }

        private eMoveDirection MoveDirection;
        private eLang Language;
        static double OneTimeSpan = 1.5;
        static double MinImgHeight = 0;
        static double MaxImgHeight = 294; 

        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += Window_SourceInitialized;
            this.Loaded += Window_Loaded;
            this.StateChanged += Window_StateChanged;
            this.Closing += Window_Closing;
            ShowInTaskbar = false;
            MoveDirection = eMoveDirection.Up;
            textBlockShoot1.PreviewMouseDown += TextBlockShoot_PreviewMouseDown;
            textBlockShoot2.PreviewMouseDown += TextBlockShoot_PreviewMouseDown;
            textBlockShoot3.PreviewMouseDown += TextBlockShoot3_PreviewMouseDown;
            textBlock.Text = GetEnglish();
        }

        private void TextBlockShoot3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Language == eLang.eng)
                textBlock.Text = GetUkr();
            else
                textBlock.Text = GetEnglish();

            if (MoveDirection == eMoveDirection.Down)
                TextBlockShoot_PreviewMouseDown(null, null);
        }

        private void TextBlockShoot_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var animation = new DoubleAnimation();
            if (MoveDirection == eMoveDirection.Up)
            {
                MoveDirection = eMoveDirection.Down;
                animation.From = imageGrid.Height;
                animation.To = MaxImgHeight;
            }
            else
            {
                MoveDirection = eMoveDirection.Up;
                animation.From = imageGrid.Height;
                animation.To = MinImgHeight;
            }

            animation.Duration = TimeSpan.FromSeconds(OneTimeSpan);
            imageGrid.BeginAnimation(HeightProperty, animation);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 100;
            this.Top = desktopWorkingArea.Bottom /2.0 - this.Height /2.0;
            ShowDesktop.AddHook(this);
        }

        private string GetEnglish()
        {
            Language = eLang.eng;
             return "- What does Marsellus Wallace look like?" +
                            Environment.NewLine + "- ..What?" +
                            Environment.NewLine + "- What country are you from!?" +
                            Environment.NewLine + "- Wha-what?" +
                            Environment.NewLine + "- What ain't no country I ever heard of! They speak English in What!?" +
                            Environment.NewLine + "- What?" +
                            Environment.NewLine + "- English, motherfucker! Do you speak it!?" +
                            Environment.NewLine + "- Yes!!" +
                            Environment.NewLine + "- Then you know what I'm saying!" +
                            Environment.NewLine + "- Yes..!" +
                            Environment.NewLine + "- Describe what Marsellus Wallace looks like!!" +
                            Environment.NewLine + "- Wha-what I—? [points gun directly in Brett's face]" +
                            Environment.NewLine + "- Say what again! SAY what again! I dare you! I double-dare you, motherfucker! Say what one more goddamn time!" +
                            Environment.NewLine + "- H-H-He's black..." +
                            Environment.NewLine + "- Go on!" +
                            Environment.NewLine + "- He's bald...!" +
                            Environment.NewLine + "- Does he look like a bitch?" +
                            Environment.NewLine + "- What? [Jules shoots Brett in the shoulder] AGHH!! Anh..!!" +
                            Environment.NewLine + "- DOES—HE—LOOK... LIKE—A BITCH!!?" +
                            Environment.NewLine + "- NO!!" +
                            Environment.NewLine + "- Then why'd you try to fuck 'im like a bitch, Brett?" +
                            Environment.NewLine + "- I didn't...!" +
                            Environment.NewLine + "- Yes, you did! YES, you DID, Brett! You tried to fuck him." +
                            Environment.NewLine + "- No... no...." +
                            Environment.NewLine + "- But Marsellus Wallace don't like to be fucked by anybody except Mrs. Wallace. You read the Bible, Brett?" +
                            Environment.NewLine + "- Yes...!" +
                            Environment.NewLine + "- Well, there's this passage I've got memorized, it sorta fits the occasion. Ezekiel 25:17: The path of the righteous man is beset on all sides by the iniquities of the selfish and the tyranny of evil men. Blessed is he who in the name of charity and good will shepherds the weak through the valley of darkness, for he is truly his brother's keeper and the finder of lost children. [begins pacing about the room] And I will strike down upon thee with great vengeance and furious anger those who attempt to poison and destroy my brothers. And you will know my name is the Lord... [pulls out his gun and aims at Brett] ...when I lay my vengeance upon thee." +
                            Environment.NewLine + "[Brett shrieks in horror as Jules and Vincent shoot him repeatedly]";
        }

        private string GetUkr()
        {
            Language = eLang.ukr;
            return "- Що виглядає Марсель Уоллес?" +
                           Environment.NewLine + "- ..Що?" +
                           Environment.NewLine + "- Яку країну ти ?!" +
                           Environment.NewLine + "- Що-чому?" +
                           Environment.NewLine + "- Що - це не та країна, про яку я колись чув! Вони говорять англійською мовою Що!" +
                           Environment.NewLine + "- що?" +
                           Environment.NewLine + "- Англійська, вилупок! Ви говорите це !?" +
                           Environment.NewLine + "- Так !!" +
                           Environment.NewLine + "- Тоді ти знаєш, що я кажу!" +
                           Environment.NewLine + "- Так ..!" +
                           Environment.NewLine + "- Опишіть, який Марсел Уоллес виглядає!" +
                           Environment.NewLine + "- Що-що я? [вказує рушницю безпосередньо в обличчя Бретта]" +
                           Environment.NewLine + "- Скажіть що знову! ГОВИЙ що знову! Я смію тебе! Я двічі смію тебе, вилупок! Скажи що ще один чортовний час!" +
                           Environment.NewLine + "- В-В-Він чорний..." +
                           Environment.NewLine + "- Давай!" +
                           Environment.NewLine + "- ... він лисий ...!" +
                           Environment.NewLine + "- він виглядає як сука?" +
                           Environment.NewLine + "- що? [Жюль стріляє Бретта в плече] Ах !! Ах .. !!" +
                           Environment.NewLine + "- ВIН ВИГЛЯДАє ЯК СУКА!!?" +
                           Environment.NewLine + "- НІ !!" +
                           Environment.NewLine + "- Тоді чому ти намагався ебать я, як сука, Бретт?" +
                           Environment.NewLine + "- я не ...!" +
                           Environment.NewLine + "- Так, ти це зробив! ТАК, ти ДИДЕ, Бретт! Ви намагалися ебать його." +
                           Environment.NewLine + "- Ні ... ні ...." +
                           Environment.NewLine + "- Але Марсельос Уоллес не люблять трахкати кого-небудь, крім пані Уоллес. Ви читаєте Біблію, Бретт?" +
                           Environment.NewLine + "- Так ...!" +
                           Environment.NewLine + "- Ну, такий уривок, який я запам'ятав, сприймається саме тоді. Єзекіїля 25:17: Шлях праведника з усіх боків осяяно беззаконнями егоїстичних і тиранії злих людей. Благословенний той, хто в ім'я благодаті та доброї волі пастух слабих через долину темряви , бо він справді є хранитель його брата і шукач загублених дітей [починає ходити по кімнаті], і я вдаритиму тебе на велику помсту і лютий гнів тих, хто намагається отруїти і знищити моїх братів. І ти дізнаєшся про мою це імя Господа ... [витягує рушницю і намагається Бретт] ... коли я покладу тобі помсту" +
                           Environment.NewLine + "[Бретт вигукує в жаху, як Жюль і Вінсент неодноразово стріляють його]";
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
