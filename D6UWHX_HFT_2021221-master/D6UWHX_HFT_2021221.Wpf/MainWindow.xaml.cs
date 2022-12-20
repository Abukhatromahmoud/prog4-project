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

namespace D6UWHX_HFT_2021221.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button1_ClickEvent(object sender, RoutedEventArgs e)
        {
            AlbumWindowModel albumWindow = new AlbumWindowModel();
            albumWindow.ShowDialog();
        }

        private void Button2_ClickEvent(object sender, RoutedEventArgs e)
        {
            ArtistWindowModel artistWindow = new ArtistWindowModel();
            artistWindow.ShowDialog();
        }

        private void Button3_ClickEvent(object sender, RoutedEventArgs e)
        {
            TrackWindowModel trackWindow = new TrackWindowModel();
            trackWindow.ShowDialog();
        }

        private void Button4_ClickEvent(object sender, RoutedEventArgs e)
        {
            NoncrudWindow noncrudWindow = new NoncrudWindow();
            noncrudWindow.ShowDialog();
        }
    }
}
