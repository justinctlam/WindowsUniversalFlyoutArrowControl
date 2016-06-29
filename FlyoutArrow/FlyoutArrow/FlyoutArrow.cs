using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FlyoutArrow.FlyoutArrow
{
    /// <summary>
    /// Extends flyout control with arrow indicator
    /// </summary>
    public sealed class FlyoutArrow : Flyout
    {
        /// <summary>
        /// The FlyoutArrowPresenter
        /// </summary>
        private readonly FlyoutArrowPresenter _flyoutArrowPresenter = new FlyoutArrowPresenter();

        /// <summary>
        /// Constructor
        /// </summary>
        public FlyoutArrow()
        {
            _flyoutArrowPresenter.MinWidth = MinWidth;
            _flyoutArrowPresenter.Style = Application.Current.Resources["FlyoutArrowPresenterStyle"] as Style;
            RegisterPropertyChangedCallback(ContentProperty, OnContentChanged);
        }

        /// <summary>
        /// The dependency property for <see cref="ArrowTopStyle"/>
        /// </summary>
        public static readonly DependencyProperty ArrowTopStyleProperty = DependencyProperty.Register(
            nameof(ArrowTopStyle),
            typeof(Style),
            typeof(FlyoutArrow),
            new PropertyMetadata(null, OnArrowArrowTopStyleChanged));

        /// <summary>
        /// The arrow style when on top
        /// </summary>
        public Style ArrowTopStyle
        {
            get { return (Style)GetValue(ArrowTopStyleProperty); }
            set { SetValue(ArrowTopStyleProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="ArrowBottomStyle"/>
        /// </summary>
        public static readonly DependencyProperty ArrowBottomStyleProperty = DependencyProperty.Register(
            nameof(ArrowBottomStyle),
            typeof(Style),
            typeof(FlyoutArrow),
            new PropertyMetadata(null, OnArrowBottomStyleChanged));

        /// <summary>
        /// The arrow style when on bottom
        /// </summary>
        public Style ArrowBottomStyle
        {
            get { return (Style)GetValue(ArrowBottomStyleProperty); }
            set { SetValue(ArrowBottomStyleProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="ArrowIndicatorHeight"/>
        /// </summary>
        public static readonly DependencyProperty ArrowIndicatorHeightProperty = DependencyProperty.Register(
            nameof(ArrowIndicatorHeight),
            typeof(double),
            typeof(FlyoutArrow),
            new PropertyMetadata(10.0, OnArrowIndicatorHeightChanged));

        /// <summary>
        /// The height of the arrow indicator
        /// </summary>
        public double ArrowIndicatorHeight
        {
            get { return (double)GetValue(ArrowIndicatorHeightProperty); }
            set { SetValue(ArrowIndicatorHeightProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="ArrowIndicatorWidth"/>
        /// </summary>
        public static readonly DependencyProperty ArrowIndicatorWidthProperty = DependencyProperty.Register(
            nameof(ArrowIndicatorWidth),
            typeof(double),
            typeof(FlyoutArrow),
            new PropertyMetadata(30.0, OnArrowIndicatorWidthChanged));

        /// <summary>
        /// The width of the arrow indicator
        /// </summary>
        public double ArrowIndicatorWidth
        {
            get { return (double)GetValue(ArrowIndicatorWidthProperty); }
            set { SetValue(ArrowIndicatorWidthProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="FlyoutLocation"/>
        /// </summary>
        public static readonly DependencyProperty FlyoutLocationProperty = DependencyProperty.Register(
            nameof(FlyoutLocation),
            typeof(FrameworkElement),
            typeof(FlyoutArrow),
            new PropertyMetadata(null, OnFlyoutLocationChanged));

        /// <summary>
        /// The location where the control will pop out
        /// </summary>
        public FrameworkElement FlyoutLocation
        {
            get { return (FrameworkElement)GetValue(FlyoutLocationProperty); }
            set { SetValue(FlyoutLocationProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="MinWidth"/>
        /// </summary>
        public static readonly DependencyProperty MinWidthProperty = DependencyProperty.Register(
            nameof(MinWidth),
            typeof(int),
            typeof(FlyoutArrow),
            new PropertyMetadata(320, OnMinWidthChanged)); // TODO: Wait for design to get back with width size

        /// <summary>
        /// The min width of the control
        /// </summary>
        public int MinWidth
        {
            get { return (int)GetValue(MinWidthProperty); }
            set { SetValue(MinWidthProperty, value); }
        }

        /// <summary>
        /// Show the flyout
        /// </summary>
        public void Show()
        {
            ShowAt(FlyoutLocation);
        }

        /// <summary>
        /// Override the Flyout's CreatePresenter to customize the UI
        /// </summary>
        /// <returns>A control of FlyoutArrowPresenter</returns>
        protected override Control CreatePresenter()
        {
            return _flyoutArrowPresenter;
        }

        /// <summary>
        /// Event for when the content changes
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="dp">DependencyProperty object</param>
        private void OnContentChanged(DependencyObject sender, DependencyProperty dp)
        {
            _flyoutArrowPresenter.Content = Content;
        }

        /// <summary>
        /// Updates the arrow's top style
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowArrowTopStyleChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrow;
            if (control != null)
            {
                control._flyoutArrowPresenter.ArrowTopStyle = (Style)dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        /// <summary>
        /// Updates the arrow's bottom style
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowBottomStyleChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrow;
            if (control != null)
            {
                control._flyoutArrowPresenter.ArrowBottomStyle = (Style)dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        /// <summary>
        /// Updates the arrow indicator height
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowIndicatorHeightChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrow;
            if (control != null)
            {
                control._flyoutArrowPresenter.ArrowIndicatorHeight = (double)dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        /// <summary>
        /// Updates the arrow indicator width
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowIndicatorWidthChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrow;
            if (control != null)
            {
                control._flyoutArrowPresenter.ArrowIndicatorWidth = (double)dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        /// <summary>
        /// Updates the flyout location element
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnFlyoutLocationChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrow;
            if (control != null)
            {
                control._flyoutArrowPresenter.FlyoutLocation = (FrameworkElement)dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        /// <summary>
        /// Updates the control's min width
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnMinWidthChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrow;
            if (control != null)
            {
                control._flyoutArrowPresenter.MinWidth = (int)dependencyPropertyChangedEventArgs.NewValue;
            }
        }
    }
}