using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
using StylesXL;
using StylesXL.Extensions;

namespace IntegraControls
{
 
    public class StepControl : Control
    {
        #region Constants

        /// <summary>
        /// Defines the types of step control.
        /// </summary>
        public enum StepControlTypes
        {
            Square,
            Saw
        }

        /// <summary>
        /// Defines the types of handles.
        /// </summary>
        public enum HandleTypes
        {
            None,
            Square,
            Circle
        }

        /// <summary>
        /// Defines the number of control points to interact with the control.
        /// </summary>
        private const int CONTROL_POINTS = 16;
        private const int CONTROL_POINT_RADIUS = 3;
        private const int CONTROL_PADDING = 10;
        private const int HIT_DISTANCE = CONTROL_POINT_RADIUS * CONTROL_POINT_RADIUS;

        /// <summary>
        /// Defines the amount of padding for control point labels.
        /// </summary>
        private const int LABEL_PADDING = 5;

        #endregion



        #region Fields

        private bool _IsInitialized = false;

        // Stores the size of the step segments
        private Size _SegmentSize;
        private Point _SegmentCenter;

        private Point _Factor;
        private Point _MouseOffset;
        private Point _Center;

        /// <summary>
        /// Stores the aspect ratio of the control.
        /// </summary>
        /// <remarks><i>Calulated based on the min width and min height properties.</i></remarks>
        private double _AspectRatio;

        /// <summary>
        /// Stores the offset to ensure all values are visible.
        /// </summary>
        /// <remarks><i>Calculated based on the min and max value signs.</i></remarks>
        private double _ValueOffset;

        /// <summary>
        /// Stores the base line to offset the control points from.
        /// </summary>
        /// <remarks><i>Calculated based on the min and max value signs.</i></remarks>
        private double _BaseLine;

        /// <summary>
        /// Determins if the base line is drawn.
        /// </summary>
        private bool _DrawBaseLine;

        /// <summary>
        /// Stores the control points to interact with the control.
        /// </summary>
        private Point[] _ControlPoints = new Point[CONTROL_POINTS];

        /// <summary>
        /// Stores the index of the selected control point.
        /// </summary>
        private int? _SelectedPoint = null;

        /// <summary>
        /// Stores the index of the selected region.
        /// </summary>
        private int? _SelectedRegion = null;

        // Stores the paths to draw the step control
        private Path _ControlPath;

        

        #endregion

        #region Constructor

        static StepControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepControl), new FrameworkPropertyMetadata(typeof(StepControl)));
        }

        public StepControl()
        {
            // Invert the render Y coordinate to start from the bottom left corner
            //this.RenderTransformOrigin = new Point(0.5, 0.5);
            //this.RenderTransform = new ScaleTransform(1, -1);

            Loaded += StepControlLoaded;

            StyleManager.StyleChanged += StyleManagerStyleChanged;
        }

        private void StyleManagerStyleChanged(object sender, EventArgs e)
        {
            InitializeStyle();
            InvalidateVisual();
        }


        #endregion

        #region Dependency Properties

        #region Dependency Properties: Registration

        public static readonly DependencyProperty HandleTypeProperty = DependencyProperty.Register(nameof(HandleType), typeof(HandleTypes), typeof(StepControl), new PropertyMetadata(HandleTypes.Circle));



        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register(nameof(Interval), typeof(double), typeof(StepControl), new PropertyMetadata(1.0));



        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(double), typeof(StepControl), new PropertyMetadata(0.0));
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(double), typeof(StepControl), new PropertyMetadata(100.0));

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type), typeof(StepControlTypes), typeof(StepControl), new PropertyMetadata(StepControlTypes.Square));

        public static readonly DependencyProperty Step01Property = DependencyProperty.Register(nameof(Step01), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step02Property = DependencyProperty.Register(nameof(Step02), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step03Property = DependencyProperty.Register(nameof(Step03), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step04Property = DependencyProperty.Register(nameof(Step04), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step05Property = DependencyProperty.Register(nameof(Step05), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step06Property = DependencyProperty.Register(nameof(Step06), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step07Property = DependencyProperty.Register(nameof(Step07), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step08Property = DependencyProperty.Register(nameof(Step08), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step09Property = DependencyProperty.Register(nameof(Step09), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step10Property = DependencyProperty.Register(nameof(Step10), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step11Property = DependencyProperty.Register(nameof(Step11), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step12Property = DependencyProperty.Register(nameof(Step12), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step13Property = DependencyProperty.Register(nameof(Step13), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step14Property = DependencyProperty.Register(nameof(Step14), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step15Property = DependencyProperty.Register(nameof(Step15), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step16Property = DependencyProperty.Register(nameof(Step16), typeof(double), typeof(StepControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));

        #endregion

        #region Dependency Properties: Implementation

        public HandleTypes HandleType
        {
            get { return (HandleTypes)GetValue(HandleTypeProperty); }
            set { SetValue(HandleTypeProperty, value); }
        }

        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public StepControlTypes Type
        {
            get { return (StepControlTypes)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public double Step01 { get { return (double)GetValue(Step01Property); } set { SetValue(Step01Property, value); } }
        public double Step02 { get { return (double)GetValue(Step02Property); } set { SetValue(Step02Property, value); } }
        public double Step03 { get { return (double)GetValue(Step03Property); } set { SetValue(Step03Property, value); } }
        public double Step04 { get { return (double)GetValue(Step04Property); } set { SetValue(Step04Property, value); } }
        public double Step05 { get { return (double)GetValue(Step05Property); } set { SetValue(Step05Property, value); } }
        public double Step06 { get { return (double)GetValue(Step06Property); } set { SetValue(Step06Property, value); } }
        public double Step07 { get { return (double)GetValue(Step07Property); } set { SetValue(Step07Property, value); } }
        public double Step08 { get { return (double)GetValue(Step08Property); } set { SetValue(Step08Property, value); } }
        public double Step09 { get { return (double)GetValue(Step09Property); } set { SetValue(Step09Property, value); } }
        public double Step10 { get { return (double)GetValue(Step10Property); } set { SetValue(Step10Property, value); } }
        public double Step11 { get { return (double)GetValue(Step11Property); } set { SetValue(Step11Property, value); } }
        public double Step12 { get { return (double)GetValue(Step12Property); } set { SetValue(Step12Property, value); } }
        public double Step13 { get { return (double)GetValue(Step13Property); } set { SetValue(Step13Property, value); } }
        public double Step14 { get { return (double)GetValue(Step14Property); } set { SetValue(Step14Property, value); } }
        public double Step15 { get { return (double)GetValue(Step15Property); } set { SetValue(Step15Property, value); } }
        public double Step16 { get { return (double)GetValue(Step16Property); } set { SetValue(Step16Property, value); } }

        #endregion

        #region Dependency Properties: Callbacks

        private static object CoerceStep(DependencyObject d, object baseValue)
        {
            StepControl control = (StepControl)d;

            double value = (double)baseValue;

            value = Math.Min(value, control.Max);
            value = Math.Max(value, control.Min);
            
            return value;
        }

        #endregion

        #endregion


        #region Event Handlers
        private int _Resolution;
        private void StepControlLoaded(object sender, RoutedEventArgs e)
        {
            MinWidth = 360;
            MinHeight = 240;

            _AspectRatio = MinWidth / MinHeight;

            double value = Interval;

            // Get the number of decimal places from the interval if smaller than 1
            while ((int)value % 10 == 0)
            {
                value *= 10;
                _Resolution++;
            }

            // Ensure the interval divides evenly into the min max range
            double range = Math.Round((Max - Min) * Math.Pow(10, _Resolution));
            double interval = Math.Round(Interval * Math.Pow(10, _Resolution));

            if(range % interval != 0)
                throw new ArgumentException("Interval doesn't divide into the min max range evenly.", nameof(Interval));

            InitializeStyle();
            InitializeMeasures();
            InitializeControlPoints();

            UpdatePath();

            _IsInitialized = true;

            InvalidateVisual();
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            Point mouse = e.GetPosition(this);
            
            int index;

            if(MouseOverControlPoint(mouse, out index))
            {
                _SelectedPoint = index;

                // Offset between mouse cursor and the control point center
                _MouseOffset = new Point(_ControlPoints[index].X - mouse.X, 
                                         _ControlPoints[index].Y - mouse.Y);

                // Update visual to highlight selected control point
                InvalidateVisual();
            }
            else if(MouseOverRegion(mouse, out index))
            {
                _SelectedRegion = index;

                _ControlPoints[(int)_SelectedRegion].Y = mouse.Y;

                UpdateValues();
                InvalidateVisual();
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            Point mouse = e.GetPosition(this);

            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(_SelectedPoint != null)
                {
                    double y = mouse.Y + _MouseOffset.Y;

                    _ControlPoints[(int)_SelectedPoint].Y = y;

                    UpdateValues();
                }
            }

            
            int index;

            if(MouseOverRegion(mouse, out index))
            {
                _SelectedRegion = index;
            }
            else
            {
                _SelectedRegion = null;
            }
            
            InvalidateVisual();
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);

            _SelectedPoint  = null;
            
            UpdateValues();
        }


        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);

            if(_SelectedRegion != null)
            {
                if (e.Delta > 0)
                {
                    _ControlPoints[(int)_SelectedRegion].Y -= _Factor.Y * Interval;
                }
                else if (e.Delta < 0)
                {
                    _ControlPoints[(int)_SelectedRegion].Y += _Factor.Y * Interval;
                }

                UpdateValues();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            _SelectedPoint = null;
            _SelectedRegion = null;

            InvalidateVisual();
        }

        #endregion

        #region Methods

        private Pen _GraphPen = new Pen();
        private Brush _ControlBrush;
        private Pen _DataPen;
        private bool _IsDark;
        private Brush _GraphBrush;

        /// <summary>
        /// Initializes the brushes, pens and colors to draw the control.
        /// </summary>
        private void InitializeStyle()
        {
            Padding = new Thickness(10, 10, 10, 10);

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                // Ensure background for rendering
                if (Background == null)
                {
                    Background = new SolidColorBrush(Colors.Transparent);

                    SolidColorBrush brush = (SolidColorBrush)Application.Current.MainWindow.Background;

                    // No window background, defaults to black
                    if (brush == null)
                    {
                        _IsDark = true;
                    }
                    else
                    {
                        if (brush.Color == Colors.Transparent)
                            _IsDark = true;
                        else
                        {
                            if (brush.GetBrightness() * ((double)brush.Color.A / 255) < 0.5)
                                _IsDark = true;
                            else
                                _IsDark = false;
                        }
                    }

                }
                else
                {
                    SolidColorBrush brush = (SolidColorBrush)Background;

                    if (brush.GetBrightness() * brush.Color.A * brush.Opacity < 0.5)
                    {
                        _IsDark = true;
                    }
                    else
                        _IsDark = false;
                }
            }
            else
            {
                Background = new SolidColorBrush(Colors.Transparent);
                
                _IsDark = true;
            }

            Console.WriteLine(_IsDark);

            _DataPen = new Pen();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _DataPen.Brush = StyleManager.Brush(StylesXL.Brushes.TestBrush);
                _ControlBrush = StyleManager.Brush(StylesXL.Brushes.TestBrush);
                _GraphBrush = StyleManager.Brush(StylesXL.Brushes.TestBrush);

                if(BorderBrush == null)
                {
                    BorderBrush = StyleManager.Brush(StylesXL.Brushes.ControlBorder);
                }
            }
            else
            {
                BorderBrush = new SolidColorBrush(Colors.Gray);
                _DataPen.Brush = new SolidColorBrush(Colors.Orange);
                _ControlBrush = new SolidColorBrush(Colors.Orange);
            }


            _DataPen.Thickness = 2;

            _GraphPen.Brush = new SolidColorBrush(Colors.Orange);
            _GraphPen.Thickness = 1;
        }

        /// <summary>
        /// Initializes the properties used for drawing calculations.
        /// </summary>
        private void InitializeMeasures()
        {
            if (ActualWidth == 0)
                return;
            // Calculate the width of a single step segment
            _SegmentSize.Width = (ActualWidth - Padding.Left - Padding.Right) / _ControlPoints.Length;

            // Factory to convert step to value
            _Factor.Y = (ActualHeight - Padding.Top - Padding.Bottom) / (Max - Min);

            // Calculates the center of a single step segment
            _SegmentCenter.X = _SegmentSize.Width / 2;

            _Center.X = (ActualWidth / 2);
            _Center.Y = (ActualHeight / 2);

            _DrawBaseLine = false;

            if (Min < 0)
            {
                if (Max > 0)
                {
                    _ValueOffset = 0;
                    _BaseLine = ActualHeight - Padding.Top + (Min * _Factor.Y);
                    _DrawBaseLine = true;
                }
                else
                {
                    _ValueOffset = Max;
                    _BaseLine = Padding.Top;
                }
            }
            else
            {
                _ValueOffset = Min;
                _BaseLine = ActualHeight - Padding.Bottom;
            }
        }

        /// <summary>
        /// Initializes the position of the control points based on step control values and type.
        /// </summary>
        private void InitializeControlPoints()
        {
            switch(Type)
            {
                case StepControlTypes.Square:
                    
                    _ControlPoints[0]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  0), (_BaseLine - ((Step01 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[1]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  1), (_BaseLine - ((Step02 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[2]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  2), (_BaseLine - ((Step03 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[3]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  3), (_BaseLine - ((Step04 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[4]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  4), (_BaseLine - ((Step05 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[5]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  5), (_BaseLine - ((Step06 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[6]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  6), (_BaseLine - ((Step07 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[7]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  7), (_BaseLine - ((Step08 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[8]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  8), (_BaseLine - ((Step09 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[9]  = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width *  9), (_BaseLine - ((Step10 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[10] = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width * 10), (_BaseLine - ((Step11 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[11] = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width * 11), (_BaseLine - ((Step12 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[12] = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width * 12), (_BaseLine - ((Step13 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[13] = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width * 13), (_BaseLine - ((Step14 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[14] = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width * 14), (_BaseLine - ((Step15 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[15] = new Point(Padding.Left + _SegmentCenter.X + (_SegmentSize.Width * 15), (_BaseLine - ((Step16 - _ValueOffset) * _Factor.Y)));

                    break;

                case StepControlTypes.Saw:

                    _ControlPoints[0]  = new Point(Padding.Left + (_SegmentSize.Width *  0), (_BaseLine - ((Step01 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[1]  = new Point(Padding.Left + (_SegmentSize.Width *  1), (_BaseLine - ((Step02 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[2]  = new Point(Padding.Left + (_SegmentSize.Width *  2), (_BaseLine - ((Step03 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[3]  = new Point(Padding.Left + (_SegmentSize.Width *  3), (_BaseLine - ((Step04 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[4]  = new Point(Padding.Left + (_SegmentSize.Width *  4), (_BaseLine - ((Step05 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[5]  = new Point(Padding.Left + (_SegmentSize.Width *  5), (_BaseLine - ((Step06 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[6]  = new Point(Padding.Left + (_SegmentSize.Width *  6), (_BaseLine - ((Step07 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[7]  = new Point(Padding.Left + (_SegmentSize.Width *  7), (_BaseLine - ((Step08 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[8]  = new Point(Padding.Left + (_SegmentSize.Width *  8), (_BaseLine - ((Step09 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[9]  = new Point(Padding.Left + (_SegmentSize.Width *  9), (_BaseLine - ((Step10 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[10] = new Point(Padding.Left + (_SegmentSize.Width * 10), (_BaseLine - ((Step11 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[11] = new Point(Padding.Left + (_SegmentSize.Width * 11), (_BaseLine - ((Step12 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[12] = new Point(Padding.Left + (_SegmentSize.Width * 12), (_BaseLine - ((Step13 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[13] = new Point(Padding.Left + (_SegmentSize.Width * 13), (_BaseLine - ((Step14 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[14] = new Point(Padding.Left + (_SegmentSize.Width * 14), (_BaseLine - ((Step15 - _ValueOffset) * _Factor.Y)));
                    _ControlPoints[15] = new Point(Padding.Left + (_SegmentSize.Width * 15), (_BaseLine - ((Step16 - _ValueOffset) * _Factor.Y)));

                    break;
            }
        }

        /// <summary>
        /// Create the geometry to draw the step control values based on the control points.
        /// </summary>
        private void UpdatePath()
        {
            StreamGeometry controlGeometry = new StreamGeometry();
            Path controlPath = new Path();

            using (StreamGeometryContext context = controlGeometry.Open())
            {
                if (Type == StepControlTypes.Square)
                {
                    context.BeginFigure(new Point(Padding.Left, _ControlPoints[0].Y), false, false);

                    for (int cp = 0, segment = 1; cp < _ControlPoints.Length - 1; cp++, segment++)
                    {
                        context.LineTo(new Point(Padding.Left + (_SegmentSize.Width * segment), _ControlPoints[cp].Y) , true, false);
                        context.LineTo(new Point(Padding.Left + (_SegmentSize.Width * segment), _ControlPoints[cp + 1].Y), true, false);
                    }

                    context.LineTo(new Point(Padding.Left + (_SegmentSize.Width * 16), _ControlPoints[15].Y), true, false);
                }
                else
                {
                    context.BeginFigure(new Point(Padding.Left, _ControlPoints[0].Y), false, false);

                    for (int cp = 1; cp < _ControlPoints.Length; cp++)
                    {
                        context.LineTo(new Point(_ControlPoints[cp].X, _ControlPoints[cp].Y), true, false);
                    }

                    context.LineTo(new Point(ActualWidth - Padding.Right, _BaseLine), true, false);
                }
            }

            controlGeometry.Freeze();
            controlPath.Data = controlGeometry;

            _ControlPath = controlPath;
        }

        private double GetValue(Point controlPoint)
        {
            double value = ((ActualHeight - Padding.Top - controlPoint.Y) / _Factor.Y) + Min;
            double remainder = (value - Min) % Interval;

            if(controlPoint == _ControlPoints[0])
            {
                Console.WriteLine(value);
                Console.WriteLine(remainder);
            }
            if (remainder >= Interval / 2)
            {
                value += Interval - remainder;
            }
            else
            {
                value -= remainder;
            }

            return value;
        }
        /// <summary>
        /// Actuates the step property values based on the control points.
        /// </summary>
        private void UpdateValues()
        {
            Step01 = GetValue(_ControlPoints[0]);
            Step02 = GetValue(_ControlPoints[1]);
            Step03 = GetValue(_ControlPoints[2]);
            Step04 = GetValue(_ControlPoints[3]);
            Step05 = GetValue(_ControlPoints[4]);
            Step06 = GetValue(_ControlPoints[5]);
            Step07 = GetValue(_ControlPoints[6]);
            Step08 = GetValue(_ControlPoints[7]);
            Step09 = GetValue(_ControlPoints[8]);
            Step10 = GetValue(_ControlPoints[9]);
            Step11 = GetValue(_ControlPoints[10]);
            Step12 = GetValue(_ControlPoints[11]);
            Step13 = GetValue(_ControlPoints[12]);
            Step14 = GetValue(_ControlPoints[13]);
            Step15 = GetValue(_ControlPoints[14]);
            Step16 = GetValue(_ControlPoints[15]);
            
            UpdatePath();
            InvalidateVisual();
        }

        private bool MouseOverControlPoint(Point mouse, out int index)
        {
            for (int i = 0; i < _ControlPoints.Length; i++)
            {
                if(Common.Math.Distance(_ControlPoints[i], mouse) < HIT_DISTANCE)
                {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        private bool MouseOverRegion(Point mouse, out int index)
        {
            if(mouse.X >= Padding.Left && mouse.X <= ActualWidth - Padding.Right)
            {
                index = Math.Min((int)((mouse.X - Padding.Left) / _SegmentSize.Width), CONTROL_POINTS - 1);
                return true;
            }

            index = -1;
            return false;
        }

        #endregion

        #region Overrides

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        /// <summary>
        /// Extends the arrange pass to maintain the control aspect ratio.
        /// </summary>
        /// <param name="arrangeBounds"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            // Maintain aspect ratio
            double sx = arrangeBounds.Height * _AspectRatio;
            double sy = arrangeBounds.Width  / _AspectRatio;

            sx = Math.Min(sx, Math.Max(arrangeBounds.Width, MinWidth));
            sy = Math.Min(sy, Math.Max(arrangeBounds.Height, MinHeight));

            return base.ArrangeOverride(new Size(sx, sy));
        }
       
        /// <summary>
        /// Extends the render pass to draw the step control.
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            InitializeMeasures();
            InitializeControlPoints();
            UpdatePath();

            base.OnRender(dc);

            if (!_IsInitialized)
                return;

            // Background
            dc.DrawRectangle(Background, null, new Rect(RenderSize));

            // Control Border
            dc.DrawRectangle(null, new Pen(BorderBrush, 1), new Rect(RenderSize));

            // Graph Border
            dc.DrawRectangle(null, new Pen(BorderBrush.Alpha(0.25), 0.5), new Rect(Padding.Left, Padding.Top, RenderSize.Width - Padding.Left - Padding.Right, RenderSize.Height - Padding.Top - Padding.Bottom));

            // Graph lines
            if (_DrawBaseLine)
            {
                dc.DrawLine(new Pen(BorderBrush.Alpha(0.25), 0.5), new Point(0, _BaseLine), new Point(RenderSize.Width, _BaseLine));
            }

            // Draw region indicators
            if (_SelectedRegion != null)
            {
                // TODO: cp null?
                Point cp = _ControlPoints[(int)_SelectedRegion];

                double x = Padding.Left + (int)_SelectedRegion * _SegmentSize.Width;

                if (Type == StepControlTypes.Square)
                {

                    double y = Math.Min(_BaseLine, cp.Y);
                    double w = _SegmentSize.Width;
                    double h = Math.Abs(_BaseLine - cp.Y);

                    Rect rect = new Rect(x, y, w, h);

                    dc.DrawRectangle(_GraphBrush.Alpha(0.25), null, rect);
                }
                else
                {
                    dc.DrawLine(new Pen(_GraphBrush.Alpha(0.25), 2), new Point(x, _BaseLine), new Point(x, cp.Y));
                }
            }


            // Step filter data
            dc.DrawGeometry(null, new Pen(_GraphBrush, 2), _ControlPath.Data);


            // Draw control points
            if (HandleType != HandleTypes.None)
            {
                if (HandleType == HandleTypes.Square)
                {
                    for (int i = 0; i < _ControlPoints.Length; i++)
                    {
                        Rect rect = new Rect(_ControlPoints[i].X - CONTROL_POINT_RADIUS, 
                                             _ControlPoints[i].Y - CONTROL_POINT_RADIUS, 
                                             CONTROL_POINT_RADIUS * 2, 
                                             CONTROL_POINT_RADIUS * 2);

                        if (i != _SelectedPoint)
                        {
                            dc.DrawRectangle(_GraphBrush, null, rect);
                        }
                        else
                        {
                            dc.DrawRectangle(_GraphBrush.Tint(), null, rect);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < _ControlPoints.Length; i++)
                    {
                        if (i != _SelectedPoint)
                        {
                            dc.DrawEllipse(_GraphBrush, null, _ControlPoints[i], CONTROL_POINT_RADIUS, CONTROL_POINT_RADIUS);
                        }
                        else
                        {
                            dc.DrawEllipse(_GraphBrush.Tint(), null, _ControlPoints[i], CONTROL_POINT_RADIUS, CONTROL_POINT_RADIUS);
                        }
                    }
                }
            }

            // Draw labels
            if(_SelectedPoint != null || _SelectedRegion != null)
            {
                string labelValue = string.Empty;
                string labelProperty = string.Empty;

                int selectedIndex = _SelectedPoint != null ? (int)_SelectedPoint : (int)_SelectedRegion;

                // Get label from property associated with control point or region
                switch (selectedIndex)
                {
                    case 0: labelValue  = Step01.ToString("N" + _Resolution.ToString()); labelProperty = "Step 1"; break;
                    case 1: labelValue  = Step02.ToString("N" + _Resolution.ToString()); labelProperty = "Step 2"; break;
                    case 2: labelValue  = Step03.ToString("N" + _Resolution.ToString()); labelProperty = "Step 3"; break;
                    case 3: labelValue  = Step04.ToString("N" + _Resolution.ToString()); labelProperty = "Step 4"; break;
                    case 4: labelValue  = Step05.ToString("N" + _Resolution.ToString()); labelProperty = "Step 5"; break;
                    case 5: labelValue  = Step06.ToString("N" + _Resolution.ToString()); labelProperty = "Step 6"; break;
                    case 6: labelValue  = Step07.ToString("N" + _Resolution.ToString()); labelProperty = "Step 7"; break;
                    case 7: labelValue  = Step08.ToString("N" + _Resolution.ToString()); labelProperty = "Step 8"; break;
                    case 8: labelValue  = Step09.ToString("N" + _Resolution.ToString()); labelProperty = "Step 9"; break;
                    case 9: labelValue  = Step10.ToString("N" + _Resolution.ToString()); labelProperty = "Step 10"; break;
                    case 10: labelValue = Step11.ToString("N" + _Resolution.ToString()); labelProperty = "Step 11"; break;
                    case 11: labelValue = Step12.ToString("N" + _Resolution.ToString()); labelProperty = "Step 12"; break;
                    case 12: labelValue = Step13.ToString("N" + _Resolution.ToString()); labelProperty = "Step 13"; break;
                    case 13: labelValue = Step14.ToString("N" + _Resolution.ToString()); labelProperty = "Step 14"; break;
                    case 14: labelValue = Step15.ToString("N" + _Resolution.ToString()); labelProperty = "Step 15"; break;
                    case 15: labelValue = Step16.ToString("N" + _Resolution.ToString()); labelProperty = "Step 16"; break;
                }

                if(labelValue != string.Empty)
                {
                    if (_SelectedPoint != null)
                    {
                        // Label transformation
                        double labelTX, labelTY;

                        // Selected control point reference
                        Point selectedPoint = _ControlPoints[(int)_SelectedPoint];

                        // Format the label text to be able to build the text geometry
                        FormattedText text = new FormattedText(labelValue,
                                                               CultureInfo.GetCultureInfo("en-us"),
                                                               FlowDirection.LeftToRight,
                                                               new Typeface(FontFamily, FontStyle, FontWeight, FontStretches.Normal),
                                                               FontSize,
                                                               _ControlBrush,
                                                               VisualTreeHelper.GetDpi(this).PixelsPerDip);

                        // Build geometry from the formatted text
                        Geometry textGeometry = text.BuildGeometry(new Point(0, 0));

                        if (selectedPoint.X < textGeometry.Bounds.Width + LABEL_PADDING * 2 + CONTROL_POINT_RADIUS)
                        {
                            // Align label right
                            labelTX = LABEL_PADDING + CONTROL_POINT_RADIUS;
                        }
                        else
                        {
                            // Align label left
                            labelTX = selectedPoint.X - textGeometry.Bounds.Width - LABEL_PADDING - CONTROL_POINT_RADIUS;
                        }

                        if (selectedPoint.Y < textGeometry.Bounds.Height + LABEL_PADDING * 2 + CONTROL_POINT_RADIUS)
                        {
                            // Align label bottom
                            labelTY = selectedPoint.Y + CONTROL_POINT_RADIUS;
                        }
                        else
                        {
                            // Align label top
                            labelTY = selectedPoint.Y - textGeometry.Bounds.Height - LABEL_PADDING * 2 - CONTROL_POINT_RADIUS;
                        }

                        TranslateTransform labelTransform = new TranslateTransform(labelTX, labelTY);

                        // Append the label transformation
                        dc.PushTransform(labelTransform);

                        // Defines the label rectangle
                        Rect labelRect = new Rect(textGeometry.Bounds.X - LABEL_PADDING,
                                                  textGeometry.Bounds.Y - LABEL_PADDING,
                                                  textGeometry.Bounds.Width + LABEL_PADDING * 2,
                                                  textGeometry.Bounds.Height + LABEL_PADDING * 2);

                        // Draw the label
                        dc.DrawRectangle(_ControlBrush.Shade(1).Alpha(0.5), null, labelRect);

                        // Draw the text
                        dc.DrawGeometry(_ControlBrush.Tint(1), null, textGeometry);

                        // Release the transform
                        dc.Pop();
                    }

                    if(_SelectedRegion != null)
                    {
                        // Format the label text to be able to build the text geometry
                        FormattedText text = new FormattedText($"{labelProperty}: {labelValue}",
                                                               CultureInfo.GetCultureInfo("en-us"),
                                                               FlowDirection.LeftToRight,
                                                               new Typeface(FontFamily, FontStyle, FontWeight, FontStretches.Normal),
                                                               FontSize,
                                                               _ControlBrush,
                                                               VisualTreeHelper.GetDpi(this).PixelsPerDip);

                        // Build geometry from the formatted text
                        Geometry textGeometry = text.BuildGeometry(new Point(0, 0));

                        TranslateTransform labelTransform = new TranslateTransform((RenderSize.Width / 2) - (textGeometry.Bounds.Width / 2 - LABEL_PADDING), RenderSize.Height - Padding.Bottom - textGeometry.Bounds.Height - LABEL_PADDING * 2);

                        
                        // Append the label transformation
                        dc.PushTransform(labelTransform);

                        // Defines the label rectangle
                        Rect labelRect = new Rect(textGeometry.Bounds.X - LABEL_PADDING,
                                                  textGeometry.Bounds.Y - LABEL_PADDING,
                                                  textGeometry.Bounds.Width + LABEL_PADDING * 2,
                                                  textGeometry.Bounds.Height + LABEL_PADDING * 2);

                        // Draw the label
                        dc.DrawRectangle(_ControlBrush.Shade(1).Alpha(0.5), null, labelRect);

                        // Draw the text
                        dc.DrawGeometry(_ControlBrush.Tint(1), null, textGeometry);

                        // Release the transform
                        dc.Pop();
                    }
                }
            }
        }

        #endregion
    }
}
