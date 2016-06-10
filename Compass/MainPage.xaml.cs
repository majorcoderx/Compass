using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Sensors;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
/// <summary>
/// Author: Nguyen Van Do
/// Date: 2015
/// </summary>

namespace Laban

{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private Compass _compass;
        private DispatcherTimer _dispatcherTimer;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string check = LocalValueControl.GetValueLocalSetting();
            int count = LocalValueControl.GetValueLocalSet();
            if (count == 4)
            {
                RatingApp();
                LocalValueControl.SetCountValueLocalSet(++count);
            }
            else if (count < 5)
            {
                LocalValueControl.SetCountValueLocalSet(++count);
            }
            if (check.Equals("none"))
            {
                LabanCheck.IsChecked = true;
                LocalValueControl.SettingLocalValue("laban");
                hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/laban.png", UriKind.Absolute));
            }
            else
            {
                if (check.Equals("laban"))
                {
                    LabanCheck.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/laban.png", UriKind.Absolute));
                }
                else if (check.Equals("laban1"))
                {
                    Laban1Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/can.png", UriKind.Absolute));
                }
                else if (check.Equals("laban2"))
                {
                    Laban2Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/cann.png", UriKind.Absolute));
                }
                else if (check.Equals("laban3"))
                {
                    Laban3Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/chan.png", UriKind.Absolute));
                }
                else if (check.Equals("laban4"))
                {
                    Laban4Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/doai.png", UriKind.Absolute));
                }
                else if (check.Equals("laban5"))
                {
                    Laban5Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/kham.png", UriKind.Absolute));
                }
                else if (check.Equals("laban6"))
                {
                    Laban6Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/khon.png", UriKind.Absolute));
                }
                else if (check.Equals("laban7"))
                {
                    Laban7Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/ly.png", UriKind.Absolute));
                }
                else
                {
                    Laban8Check.IsChecked = true;
                    hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/ton.png", UriKind.Absolute));
                }
            }

            Rotate();
            try
            {
                _dispatcherTimer.Start();
            }
            catch (Exception)
            {
                ShutdownApp();
            }

        }

        private async void RatingApp()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + Windows.ApplicationModel.Store.CurrentApp.AppId));
        }

        public void Rotate()
        {
            _compass = Compass.GetDefault();
            if (_compass != null)
            {
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Tick += DisplayCompass;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            }
        }

        private void DisplayCompass(object sender, object args)
        {
            CompassReading reading = _compass.GetCurrentReading();
            if (reading != null && turnOnCompass)
            {

                double toado = Math.Round(reading.HeadingMagneticNorth, 1);
                ((RotateTransform)hinhlaban.RenderTransform).Angle = -toado;
                // System.Diagnostics.Debug.WriteLine(reading.HeadingTrueNorth);
                if (toado >= 0 && toado <= 22.5 || toado > 337.5 && toado <= 360)
                {
                    Huong.Text = "Bắc " + (int)toado;
                    if (toado >= 0 && toado <= 22.5)
                    {
                        Toa.Text = "Tọa: Nam " + (180 + (int)toado);
                    }
                    else
                    {
                        Toa.Text = "Tọa: Nam " + ((int)toado - 180);
                    }
                }
                else if (toado > 22.5 && toado <= 67.5)
                {
                    Huong.Text = "Đông Bắc " + (int)toado;
                    Toa.Text = "Tọa: Tây Nam " + (180 + (int)toado);
                }
                else if (toado > 67.5 && toado <= 112.5)
                {
                    Huong.Text = "Đông " + (int)toado;
                    Toa.Text = "Tọa: Tây " + (180 + (int)toado);
                }
                else if (toado > 112.5 && toado <= 157.5)
                {
                    Huong.Text = "Đông Nam " + (int)toado;
                    Toa.Text = "Tọa: Tây Bắc " + (180 + (int)toado);
                }
                else if (toado > 157.5 && toado <= 202.5)
                {
                    Huong.Text = "Nam " + (int)toado;
                    if (toado <= 180)
                    {
                        Toa.Text = "Tọa: Bắc " + (180 + (int)toado);
                    }
                    else Toa.Text = "Tọa: Bắc " + ((int)toado - 180);
                }
                else if (toado > 202.5 && toado <= 247.5)
                {
                    Huong.Text = "Tây Nam " + (int)toado;
                    Toa.Text = "Tọa: Đông Bắc " + ((int)toado - 180);
                }
                else if (toado > 247.5 && toado <= 292.5)
                {
                    Huong.Text = "Tây " + (int)toado;
                    Toa.Text = "Tọa: Đông " + ((int)toado - 180);
                }
                else
                {
                    Huong.Text = "Tây Bắc " + (int)toado;
                    Toa.Text = "Tọa: Đông Nam " + ((int)toado - 180);
                }

            }
        }

        private async void ShutdownApp()
        {
            MessageDialog msg = new MessageDialog("Điện thoại không có cảm biến la bàn!", "Thoát ứng dụng");
            msg.Commands.Add(new UICommand("Ok", Oke_Click));
            await msg.ShowAsync();
        }

        private void Oke_Click(IUICommand command)
        {
            Application.Current.Exit();
        }

        private void ChooseCompass_Click(object sender, RoutedEventArgs e)
        {
            if (openMenu)
            {
                MenuChoose.IsOpen = false;
                openMenu = false;
            }
            else
            {
                MenuChoose.IsOpen = true;
                openMenu = true;
            }
        }

        // Turn off compass
        private void TurnoffCompass_Click(object sender, RoutedEventArgs e)
        {
            if (turnOnCompass)
            {
                turnOnCompass = false;
                TurnOffCompass.Content = "Mở la bàn";
            }
            else
            {
                turnOnCompass = true;
                TurnOffCompass.Content = "Tắt la bàn";
            }
        }

        private void ConfirmChooseCompass_Click(object sender, RoutedEventArgs e)
        {
            MenuChoose.IsOpen = false;
            openMenu = false;
        }

        private bool openMenu = false;
        private bool turnOnCompass = true;

        // Clear compass type checked
        private void ClearCheck(int val)
        {
            if (val == 0)
            {
                Laban8Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                Laban1Check.IsChecked = false;
            }
            else if (val == 1)
            {
                Laban8Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
            else if (val == 2)
            {
                Laban8Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban1Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
            else if (val == 3)
            {
                Laban8Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban1Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
            else if (val == 4)
            {
                Laban8Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban1Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
            else if (val == 5)
            {
                Laban8Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban1Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
            else if (val == 6)
            {
                Laban8Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban1Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
            else if (val == 7)
            {
                Laban8Check.IsChecked = false;
                Laban1Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
            else
            {
                Laban1Check.IsChecked = false;
                Laban7Check.IsChecked = false;
                Laban6Check.IsChecked = false;
                Laban5Check.IsChecked = false;
                Laban4Check.IsChecked = false;
                Laban3Check.IsChecked = false;
                Laban2Check.IsChecked = false;
                LabanCheck.IsChecked = false;
            }
        }

        private void Laban_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(0);
            LabanCheck.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/laban.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban");
        }

        private void Laban1_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(1);
            Laban1Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/can.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban1");
        }

        private void Laban2_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(2);
            Laban2Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/cann.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban2");
        }

        private void Laban3_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(3);
            Laban3Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/chan.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban3");
        }

        private void Laban4_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(4);
            Laban4Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/doai.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban4");
        }

        private void Laban5_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(5);
            Laban5Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/kham.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban5");
        }

        private void Laban6_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(6);
            Laban6Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/khon.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban6");
        }

        private void Laban7_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(7);
            Laban7Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/ly.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban7");
        }

        private void Laban8_Checked(object sender, RoutedEventArgs e)
        {
            ClearCheck(8);
            Laban8Check.IsChecked = true;
            hinhlaban.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/ton.png", UriKind.Absolute));
            LocalValueControl.SettingLocalValue("laban8");
        }

        private bool chooseOld = false;
        private bool checkMale = false;
        private bool checkFemale = false;

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!chooseOld)
            {
                chooseOld = true;
                Choose_Old.IsOpen = true;
            }
            else
            {
                chooseOld = false;
                Choose_Old.IsOpen = false;
            }
        }

        private void Female_Checked(object sender, RoutedEventArgs e)
        {
            checkFemale = true;
            checkMale = false;
            Female.IsChecked = true;
            Male.IsChecked = false;
        }

        private void Male_Checked(object sender, RoutedEventArgs e)
        {
            checkMale = true;
            checkFemale = false;
            Female.IsChecked = false;
            Male.IsChecked = true;
        }

        private void ResultCungmenh_Click(object sender, RoutedEventArgs e)
        {
            int total = 0;
            if (InputOld.Text.Contains("."))
            {
                int _index = InputOld.Text.LastIndexOf(".");
                string newString = InputOld.Text.Substring(0, _index);
                char[] text = newString.ToCharArray();
                foreach (char a in text)
                {
                    total += Convert.ToInt32(new String(a, 1));
                }
            }
            else
            {
                char[] text = InputOld.Text.ToCharArray();
                foreach (char a in text)
                {
                    total += Convert.ToInt32(new String(a, 1));
                }
            }
            // Calculate for Cung Menh
            int val = total % 9;
            System.Diagnostics.Debug.WriteLine(total);
            if (checkFemale)
            {
                switch (val)
                {
                    case 1:
                        OutputCungmenh.Text = "Cung Khảm";
                        break;
                    case 2:
                        OutputCungmenh.Text = "Cung Ly";
                        break;
                    case 3:
                        OutputCungmenh.Text = "Cung Cấn";
                        break;
                    case 4:
                        OutputCungmenh.Text = "Cung Đoài";
                        break;
                    case 5:
                        OutputCungmenh.Text = "Cung Càn";
                        break;
                    case 6:
                        OutputCungmenh.Text = "Cung Khôn";
                        break;
                    case 7:
                        OutputCungmenh.Text = "Cung Tốn";
                        break;
                    case 8:
                        OutputCungmenh.Text = "Cung Chấn";
                        break;
                    case 0:
                        OutputCungmenh.Text = "Cung Khôn";
                        break;
                }
            }
            else if (checkMale)
            {
                switch (val)
                {
                    case 1:
                        OutputCungmenh.Text = "Cung Cấn";
                        break;
                    case 2:
                        OutputCungmenh.Text = "Cung Càn";
                        break;
                    case 3:
                        OutputCungmenh.Text = "Cung Đoài";
                        break;
                    case 4:
                        OutputCungmenh.Text = "Cung Cấn";
                        break;
                    case 5:
                        OutputCungmenh.Text = "Cung Ly";
                        break;
                    case 6:
                        OutputCungmenh.Text = "Cung Khảm";
                        break;
                    case 7:
                        OutputCungmenh.Text = "Cung Khôn";
                        break;
                    case 8:
                        OutputCungmenh.Text = "Cung Chấn";
                        break;
                    case 0:
                        OutputCungmenh.Text = "Cung Tốn";
                        break;
                }
            }
            else OutputCungmenh.Text = "Chưa biết giới";
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Choose_Old.IsOpen = false;
            chooseOld = false;
        }

        private void Female_Unchecked(object sender, RoutedEventArgs e)
        {
            checkFemale = false;
        }

        private void Male_Unchecked(object sender, RoutedEventArgs e)
        {
            checkMale = false;
        }
    }

}
