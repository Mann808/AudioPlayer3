using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Policy;
using System.Windows.Threading;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace AudioPlayer3
{
    public partial class MainWindow : Window
    {
        private MediaPlayer player = new MediaPlayer();
        TimeSpan _position;
        DispatcherTimer _timer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            player.MediaOpened += media_MediaOpened;
            player.MediaEnded += Player_MediaEnded1;
            player.Volume = 100;
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(timerTick);
            _timer.Start();
        }

        private void Player_MediaEnded1(object sender, EventArgs e)
        {
            if (lbMusic.SelectedItem == lbMusic.Items[lbMusic.Items.Count - 1])
            {
                lbMusic.SelectedItem = lbMusic.Items[0];
            }
            else
            {
                lbMusic.SelectedItem = lbMusic.Items[lbMusic.SelectedIndex + 1];
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (lbMusic.SelectedItem == lbMusic.Items[lbMusic.Items.Count - 1])
            {
                lbMusic.SelectedItem = lbMusic.Items[0];
            }
            else
            {
                lbMusic.SelectedItem = lbMusic.Items[lbMusic.SelectedIndex + 1];
            }
        }


        private void media_MediaOpened(object sender, EventArgs e)
        {
            _position = player.NaturalDuration.TimeSpan;
            sliderMusic.Minimum = 0;
            sliderMusic.Maximum = _position.TotalSeconds;

        }

        private void MusicListUppdate()
        {
            lbMusic.ItemsSource = null;
            lbMusic.Items.Clear();
            lbMusic.ItemsSource = music.Name;
        }

        private void LbMusic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMusic.SelectedItem != null)
            {
                player.Open(new Uri(music.patchs[lbMusic.SelectedIndex]));
                player.Play();
            }
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            if (lbMusic.SelectedIndex != lbMusic.Items.Count)
            {
                player.Open(new Uri(music.patchs[lbMusic.SelectedIndex + 1]));
            }
            else
            {
                player.Open(new Uri(music.patchs[0]));
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            sliderMusic.Value = player.Position.TotalSeconds;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            addfiles.AddMusicCollection();
            MusicListUppdate();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void LbMusic_Loaded(object sender, RoutedEventArgs e)
        {
            if (lbMusic.SelectedItem != null)
            {
                lbMusic.SelectedItem = lbMusic.Items[lbMusic.SelectedIndex];
                MusicListUppdate();
            }
        }

        private void SliderMusic_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int pos = Convert.ToInt32(sliderMusic.Value);
            player.Position = new TimeSpan(0, 0, 0, pos, 0);
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Volume = sliderVolume.Value / 100;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            btnPlay.Visibility = Visibility.Hidden;
            btnPause.Visibility = Visibility.Visible;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            btnPlay.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Hidden;
        }

        private void btnAgain_Click(object sender, RoutedEventArgs e)
        {

            MusicListUppdate();
            for (int i = 0; i < music.Name.Count; i++)
            {
                string tmp = music.Name[i];
                music.Name.RemoveAt(i);
                music.Name.Insert(0, tmp);
            }

            for (int i = 0; i < music.patchs.Count; i++)
            {
                string tmp = music.patchs[i];
                music.patchs.RemoveAt(i);
                music.patchs.Insert(0, tmp);
            }
            MusicListUppdate();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (lbMusic.SelectedItem == lbMusic.Items[0])
            {
                lbMusic.SelectedItem = lbMusic.Items[music.patchs.Count - 1];
            }
            else
            {
                lbMusic.SelectedItem = lbMusic.Items[lbMusic.SelectedIndex - 1];
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            lbMusic.SelectedItem = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            Random RND = new Random();


            for (int i = 0; i < music.Name.Count; i++)
            {
                string tmp = music.Name[i];
                music.Name.RemoveAt(i);
                music.Name.Insert(RND.Next(music.Name.Count), tmp);
            }

            for (int i = 0; i < music.patchs.Count; i++)
            {
                string tmp = music.patchs[i];
                music.patchs.RemoveAt(i);
                music.patchs.Insert(RND.Next(music.patchs.Count), tmp);
            }

            MusicListUppdate();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
