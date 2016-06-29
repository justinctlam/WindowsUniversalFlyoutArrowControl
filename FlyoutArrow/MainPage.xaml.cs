using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FlyoutArrow.FlyoutArrow;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlyoutArrow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var flyoutArrow = new FlyoutArrow.FlyoutArrow
            {
                Content = new MyUserControl(),
                ArrowTopStyle = Application.Current.Resources["FlyoutArrowStyle"] as Style,
                ArrowBottomStyle = Application.Current.Resources["FlyoutArrowStyle"] as Style,
                MinWidth = 200,
                FlyoutLocation = sender as FrameworkElement
            };

            flyoutArrow.Show();
        }
    }
}
