using System.Windows;
using Microsoft.Practices.Prism.Events;

namespace SET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            var eventAggregator = new EventAggregator();

            _mainWindowViewModel = new MainWindowViewModel(eventAggregator);

            DataContext = _mainWindowViewModel;
        }
    }
}
