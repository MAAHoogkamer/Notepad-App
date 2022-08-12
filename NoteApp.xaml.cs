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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;
using Microsoft.Win32;
using System.IO;

namespace WPF_Uebung
{
    public partial class NoteApp : Window, INotifyPropertyChanged
    {
        public NoteApp()
        {
            InitializeComponent();
            DataContext = this;
        }
            private Brush _DiscC = Brushes.Ivory;
            public Brush DiscC
        {
            get { return _DiscC; }
            set
            {
                _DiscC = value;
                OnPropertyChanged("DiscC"); 
            }
        }
        public string _GoStop = "Go";
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string Hello)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Hello));
        }
        #endregion
        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Close();
        }
        double SpeedS;
        CancellationTokenSource DiscoToken = new();
        private async void DisCode(object SpeedS) 
        {
            string? str_SpeedS = SpeedS.ToString();
            int int_SpeedS = int.Parse(str_SpeedS);
            DiscoToken = new CancellationTokenSource();
            do
            {
                Brush[] arr = { Brushes.Indigo, Brushes.Cyan, Brushes.GreenYellow, Brushes.Magenta, Brushes.Aqua, Brushes.DeepPink, Brushes.Gold, Brushes.DarkOrchid, Brushes.HotPink };
                foreach (Brush s in arr)
                {
                    if (DiscoToken.IsCancellationRequested)
                    {
                        break;
                    }
                    DiscC = s;
                    Task.Delay(int_SpeedS).Wait();
                }
                if (DiscoToken.IsCancellationRequested)
                {
                    DiscC = Brushes.Ivory;
                    break;
                }
                await Task.Yield();
            } while (true);
        }
        int i = 0;
        int ii = 0;
        private async void Disco(object sender, EventArgs e)
        {
            if (i == 0)
            {
                GoStop.Content = "Stop";
                Thread a = new Thread(DisCode);
                a.IsBackground = true;
                a.Start(SpeedS);
                i=1;
            } else if (i == 1)
            {
                GoStop.Content = "Go!";
                DiscoToken.Cancel();
                i=0;
            }
            await Task.Delay(1);
        }
        private async void DiscoOption(object sender, EventArgs e)
        {
            if (ii == 0)
            {
                this.DiscoPanel.Visibility = Visibility.Visible;
                ii = 1;
            }
            else if (ii == 1)
            {
                this.DiscoPanel.Visibility = Visibility.Hidden;
                ii = 0;
            }
        }
        private void SliderS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SpeedS = (int)SliderS.Value;
        }
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openText = new OpenFileDialog();
            if (openText.ShowDialog() == true)
                editBox.Text = File.ReadAllText(openText.FileName);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveText = new SaveFileDialog();
            if (saveText.ShowDialog() == true)
                File.WriteAllText(saveText.FileName, editBox.Text);
        }
    }
}
    

