using System;
using System.Windows;
using PropertyChanged;

namespace Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    [ImplementPropertyChanged]
    public partial class MainWindow : Window, IDisposable
    {
        public ChatClient Client { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Client = new ChatClient();
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}