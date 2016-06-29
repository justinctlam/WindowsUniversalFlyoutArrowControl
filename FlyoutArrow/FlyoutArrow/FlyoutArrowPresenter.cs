using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace FlyoutArrow.FlyoutArrow
{
    /// <summary>
    /// FlyoutArrowPresenter class that adds an arrow to the FlyoutPresenter
    /// </summary>
    [TemplatePart(Name = "ControlCanvas", Type = typeof(Canvas))]
    [TemplatePart(Name = "Arrow", Type = typeof(Polygon))]
    [TemplatePart(Name = "Content", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "ArrowOutlineLeft", Type = typeof(Line))]
    [TemplatePart(Name = "ArrowOutlineRight", Type = typeof(Line))]
    public class FlyoutArrowPresenter : FlyoutPresenter
    {
        /// <summary>
        /// Canvas control element
        /// </summary>
        private Canvas _controlCanvas;

        /// <summary>
        /// Arrow control element
        /// </summary>
        private Polygon _arrow;

        /// <summary>
        /// FlyoutContent control element
        /// </summary>
        private ContentPresenter _content;

        /// <summary>
        /// Arrow outline element
        /// </summary>
        private Line _arrowOutlineLeft;
        private Line _arrowOutlineRight;

        /// <summary>
        /// The position of the content from the top of the canvas, used to create space for the arrow
        /// </summary>
        private double _topPosition;

        /// <summary>
        /// The dependency property for <see cref="ArrowTopStyle"/>
        /// </summary>
        public static readonly DependencyProperty ArrowTopStyleProperty = DependencyProperty.Register(
            nameof(ArrowTopStyle),
            typeof(Style),
            typeof(FlyoutArrowPresenter),
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
            typeof(FlyoutArrowPresenter),
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
            typeof(FlyoutArrowPresenter),
            new PropertyMetadata(10.0, OnArrowIndicatorHeightChanged));

        /// <summary>
        /// The arrow indicator height
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
            typeof(FlyoutArrowPresenter),
            new PropertyMetadata(30.0, OnArrowIndicatorWidthChanged));

        /// <summary>
        /// The arrow indicator width
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
            typeof(FlyoutArrowPresenter),
            new PropertyMetadata(null, OnFlyoutLocationChanged));

        /// <summary>
        /// The location where the control will pop out at
        /// </summary>
        public FrameworkElement FlyoutLocation
        {
            get { return (FrameworkElement)GetValue(FlyoutLocationProperty); }
            set { SetValue(FlyoutLocationProperty, value); }
        }

        /// <summary>
        /// Initialize the controls when the template is applied
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (_controlCanvas != null)
            {
                _controlCanvas.LayoutUpdated -= OnLayoutUpdated;
            }

            _controlCanvas = GetTemplateChild("ControlCanvas") as Canvas;
            if (_controlCanvas != null)
            {
                _controlCanvas.LayoutUpdated += OnLayoutUpdated;
            }

            _content = GetTemplateChild("Content") as ContentPresenter;
            _arrow = GetTemplateChild("Arrow") as Polygon;

            _arrowOutlineLeft = GetTemplateChild("ArrowOutlineLeft") as Line;
            _arrowOutlineRight = GetTemplateChild("ArrowOutlineRight") as Line;
        }

        /// <summary>
        /// Update the control when a layout occurs
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Argument object</param>
        private void OnLayoutUpdated(object sender, object e)
        {
            if (_content != null)
            {
                _content.Width = ActualWidth;
            }

            UpdateTopPosition();
            UpdateArrowPoints();
            UpdateArrowStyle();
            UpdateFlyoutHeight();
        }

        /// <summary>
        /// Updates the arrow's top style
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowArrowTopStyleChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrowPresenter;
            control?.UpdateArrowStyle();
        }

        /// <summary>
        /// Updates the arrow's bottom style
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowBottomStyleChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrowPresenter;
            control?.UpdateArrowStyle();
        }

        /// <summary>
        /// Updates the control's positioning and arrow placement
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnFlyoutLocationChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrowPresenter;
            if (control != null)
            {
                control.UpdateTopPosition();
                control.UpdateArrowPoints();
                control.UpdateArrowStyle();
            }
        }

        /// <summary>
        /// Updates the control's height
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowIndicatorHeightChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrowPresenter;
            control?.UpdateFlyoutHeight();
        }

        /// <summary>
        /// Updates the control's arrow polygon points
        /// </summary>
        /// <param name="dependencyObject">The control</param>
        /// <param name="dependencyPropertyChangedEventArgs">The change arguments</param>
        private static void OnArrowIndicatorWidthChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as FlyoutArrowPresenter;
            if (control != null)
            {
                control.UpdateArrowPoints();
                control.UpdateArrowStyle();
            }
        }

        /// <summary>
        /// Updates the height of the control
        /// </summary>
        private void UpdateFlyoutHeight()
        {
            if (_controlCanvas != null && _content != null)
            {
                _controlCanvas.Height = _content.ActualHeight + ArrowIndicatorHeight;
            }
        }

        /// <summary>
        /// Updates the arrow styling based on location
        /// </summary>
        private void UpdateArrowStyle()
        {
            if (FlyoutLocation != null &&  _controlCanvas != null && _arrow != null)
            {
                var targetUI = FlyoutLocation.TransformToVisual(_controlCanvas);
                var screenCoords = targetUI.TransformPoint(new Point());
                _arrow.Style = (screenCoords.Y >= 0) ? ArrowBottomStyle : ArrowTopStyle;
            }
        }

        /// <summary>
        /// Updates the points for drawing the polygon arrow
        /// </summary>
        private void UpdateArrowPoints()
        {
            if (FlyoutLocation != null && _content != null && _controlCanvas != null && _arrow != null)
            {
                // Get the coordinates of the canvas relative to the flyout location
                var targetUiTransform = FlyoutLocation.TransformToVisual(_controlCanvas);
                var screenCoords = targetUiTransform.TransformPoint(new Point());

                // Determine the x, y location of the arrow
                // and the direction the arrow points towards to.
                // yDirection of -1 indicates the direction of the arrow is drawn upwards
                // yDirection of 1 indicates the direction of the arrow is drawn downwards
                // If the arrow is at the top, the base of the arrow aligns with the top
                // edge of the flyout content.
                // If the arrow is at the bottom, the base of the arrow aligns with the
                // bottom edge of the flyout content.
                var yDirection = -1;
                var yArrowBasePosition = _topPosition;
                if (screenCoords.Y >= 0)
                {
                    yDirection = 1;
                    yArrowBasePosition = _content.ActualHeight;
                }

                var yOffset = yDirection * 2;
                var halfFlyoutLocationWidth = FlyoutLocation.ActualWidth / 2.0;
                var halfArrowIndicatorWidth = ArrowIndicatorWidth / 2.0;

                var points = new PointCollection
                {
                    new Point(screenCoords.X - halfArrowIndicatorWidth + halfFlyoutLocationWidth, (yArrowBasePosition - yOffset)),
                    new Point(screenCoords.X + halfArrowIndicatorWidth + halfFlyoutLocationWidth, (yArrowBasePosition - yOffset)),
                    new Point(screenCoords.X + halfFlyoutLocationWidth, yArrowBasePosition + ArrowIndicatorHeight * yDirection)
                };

                _arrow.Points = points;

                var deltaX1 = (points[2].X - points[0].X);
                var deltaX2 = (points[2].X - points[1].X);
                var slope1 = (deltaX1 != 0.0) ? (points[2].Y - points[0].Y) / deltaX1 : 0;
                var slope2 = (deltaX2 != 0.0) ? (points[2].Y - points[1].Y) / deltaX2 : 0;

                // The math here is to calculate the point from the tip of the arrow to the base of the
                // popup box. We do this because the actual triangle beak's base is slighting offset into the
                // popup box to cover the outline of the popup box. The goal is to get the ouline around
                // the popup box and triangle as one item.
                _arrowOutlineLeft.X1 = points[2].X;
                _arrowOutlineLeft.Y1 = points[2].Y;
                _arrowOutlineLeft.X2 = (slope1 != 0.0) ? (yArrowBasePosition - points[2].Y) / slope1 + points[2].X : 0;
                _arrowOutlineLeft.Y2 = points[0].Y + yOffset;

                _arrowOutlineRight.X1 = points[2].X;
                _arrowOutlineRight.Y1 = points[2].Y;
                _arrowOutlineRight.X2 = (slope1 != 0.0) ? (yArrowBasePosition - points[2].Y) / slope2 + points[2].X : 0;
                _arrowOutlineRight.Y2 = points[1].Y + yOffset;
            }
        }

        /// <summary>
        /// Updates the position offset vertically from the top of the canvas
        /// </summary>
        private void UpdateTopPosition()
        {
            if (FlyoutLocation != null && _content != null && _controlCanvas != null)
            {
                var targetUiTransform = FlyoutLocation.TransformToVisual(_controlCanvas);
                var screenCoords = targetUiTransform.TransformPoint(new Point());

                _topPosition = (screenCoords.Y >= 0) ? 0 : ArrowIndicatorHeight;
                Canvas.SetTop(_content, _topPosition);
            }
        }
    }
}