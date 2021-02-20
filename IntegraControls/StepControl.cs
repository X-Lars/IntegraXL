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
        private int _ValueOffset;

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
        }


        #endregion

        #region Dependency Properties

        #region Dependency Properties: Registration

        public static readonly DependencyProperty HandleTypeProperty = DependencyProperty.Register(nameof(HandleType), typeof(HandleTypes), typeof(StepControl), new PropertyMetadata(HandleTypes.Circle));

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(int), typeof(StepControl), new PropertyMetadata(0));
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(int), typeof(StepControl), new PropertyMetadata(127));

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type), typeof(StepControlTypes), typeof(StepControl), new PropertyMetadata(StepControlTypes.Square));

        public static readonly DependencyProperty Step01Property = DependencyProperty.Register(nameof(Step01), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step02Property = DependencyProperty.Register(nameof(Step02), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step03Property = DependencyProperty.Register(nameof(Step03), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step04Property = DependencyProperty.Register(nameof(Step04), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step05Property = DependencyProperty.Register(nameof(Step05), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step06Property = DependencyProperty.Register(nameof(Step06), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step07Property = DependencyProperty.Register(nameof(Step07), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step08Property = DependencyProperty.Register(nameof(Step08), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step09Property = DependencyProperty.Register(nameof(Step09), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step10Property = DependencyProperty.Register(nameof(Step10), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step11Property = DependencyProperty.Register(nameof(Step11), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step12Property = DependencyProperty.Register(nameof(Step12), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step13Property = DependencyProperty.Register(nameof(Step13), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step14Property = DependencyProperty.Register(nameof(Step14), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step15Property = DependencyProperty.Register(nameof(Step15), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));
        public static readonly DependencyProperty Step16Property = DependencyProperty.Register(nameof(Step16), typeof(int), typeof(StepControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceStep));

        #endregion

        #region Dependency Properties: Implementation

        public HandleTypes HandleType
        {
            get { return (HandleTypes)GetValue(HandleTypeProperty); }
            set { SetValue(HandleTypeProperty, value); }
        }

        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public StepControlTypes Type
        {
            get { return (StepControlTypes)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public int Step01 { get { return (int)GetValue(Step01Property); } set { SetValue(Step01Property, value); } }
        public int Step02 { get { return (int)GetValue(Step02Property); } set { SetValue(Step02Property, value); } }
        public int Step03 { get { return (int)GetValue(Step03Property); } set { SetValue(Step03Property, value); } }
        public int Step04 { get { return (int)GetValue(Step04Property); } set { SetValue(Step04Property, value); } }
        public int Step05 { get { return (int)GetValue(Step05Property); } set { SetValue(Step05Property, value); } }
        public int Step06 { get { return (int)GetValue(Step06Property); } set { SetValue(Step06Property, value); } }
        public int Step07 { get { return (int)GetValue(Step07Property); } set { SetValue(Step07Property, value); } }
        public int Step08 { get { return (int)GetValue(Step08Property); } set { SetValue(Step08Property, value); } }
        public int Step09 { get { return (int)GetValue(Step09Property); } set { SetValue(Step09Property, value); } }
        public int Step10 { get { return (int)GetValue(Step10Property); } set { SetValue(Step10Property, value); } }
        public int Step11 { get { return (int)GetValue(Step11Property); } set { SetValue(Step11Property, value); } }
        public int Step12 { get { return (int)GetValue(Step12Property); } set { SetValue(Step12Property, value); } }
        public int Step13 { get { return (int)GetValue(Step13Property); } set { SetValue(Step13Property, value); } }
        public int Step14 { get { return (int)GetValue(Step14Property); } set { SetValue(Step14Property, value); } }
        public int Step15 { get { return (int)GetValue(Step15Property); } set { SetValue(Step15Property, value); } }
        public int Step16 { get { return (int)GetValue(Step16Property); } set { SetValue(Step16Property, value); } }

        #endregion

        #region Dependency Properties: Callbacks

        private static object CoerceStep(DependencyObject d, object baseValue)
        {
            StepControl control = (StepControl)d;

            int value = (int)baseValue;

            value = Math.Min(value, control.Max);
            value = Math.Max(value, control.Min);
            
            return value;
        }

        #endregion

        #endregion

        
        #region Event Handlers

        private void StepControlLoaded(object sender, RoutedEventArgs e)
        {
            MinWidth = 240;
            MinHeight = 160;

            _AspectRatio = MinWidth / MinHeight;


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
                _SelectedPoint  = index;

                // Offset between mouse cursor and the control point center
                _MouseOffset = new Point(_ControlPoints[index].X - mouse.X, 
                                         _ControlPoints[index].Y - mouse.Y);

                // Update visual to highlight selected control point
                InvalidateVisual();
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
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

            if(_SelectedPoint != null)
            {
                _SelectedRegion = _SelectedPoint;
            }
            else
            {
                int index;

                if(MouseOverRegion(mouse, out index))
                {
                    _SelectedRegion = index;
                }
                else
                {
                    _SelectedRegion = null;
                }
            }

            InvalidateVisual();
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            _SelectedPoint  = null;

            InvalidateVisual();
        }


        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);

            if(_SelectedRegion != null)
            {
                if(e.Delta > 0)
                {
                    _ControlPoints[(int)_SelectedRegion].Y -= Math.Ceiling(_Factor.Y);
                }
                else
                {
                    _ControlPoints[(int)_SelectedRegion].Y += Math.Ceiling(_Factor.Y);
                }

                UpdateValues();
                InvalidateVisual();
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
                _GraphBrush = StyleManager.Brush(StylesXL.Brushes.ControlSelected);

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

                    context.LineTo(new Point(Padding.Left +(_SegmentSize.Width * 16), _ControlPoints[15].Y), true, false);
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

        /// <summary>
        /// Actuates the step property values based on the control points.
        /// </summary>
        private void UpdateValues()
        {
            Step01 = (int)((ActualHeight - Padding.Top - _ControlPoints[0].Y)  / _Factor.Y) + Min;
            Step02 = (int)((ActualHeight - Padding.Top - _ControlPoints[1].Y ) / _Factor.Y) + Min;
            Step03 = (int)((ActualHeight - Padding.Top - _ControlPoints[2].Y ) / _Factor.Y) + Min;
            Step04 = (int)((ActualHeight - Padding.Top - _ControlPoints[3].Y ) / _Factor.Y) + Min;
            Step05 = (int)((ActualHeight - Padding.Top - _ControlPoints[4].Y ) / _Factor.Y) + Min;
            Step06 = (int)((ActualHeight - Padding.Top - _ControlPoints[5].Y ) / _Factor.Y) + Min;
            Step07 = (int)((ActualHeight - Padding.Top - _ControlPoints[6].Y ) / _Factor.Y) + Min;
            Step08 = (int)((ActualHeight - Padding.Top - _ControlPoints[7].Y ) / _Factor.Y) + Min;
            Step09 = (int)((ActualHeight - Padding.Top - _ControlPoints[8].Y ) / _Factor.Y) + Min;
            Step10 = (int)((ActualHeight - Padding.Top - _ControlPoints[9].Y ) / _Factor.Y) + Min;
            Step11 = (int)((ActualHeight - Padding.Top - _ControlPoints[10].Y) / _Factor.Y) + Min;
            Step12 = (int)((ActualHeight - Padding.Top - _ControlPoints[11].Y) / _Factor.Y) + Min;
            Step13 = (int)((ActualHeight - Padding.Top - _ControlPoints[12].Y) / _Factor.Y) + Min;
            Step14 = (int)((ActualHeight - Padding.Top - _ControlPoints[13].Y) / _Factor.Y) + Min;
            Step15 = (int)((ActualHeight - Padding.Top - _ControlPoints[14].Y) / _Factor.Y) + Min;
            Step16 = (int)((ActualHeight - Padding.Top - _ControlPoints[15].Y) / _Factor.Y) + Min;

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
                index = (int)((mouse.X - Padding.Left) / _SegmentSize.Width);
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
                    case 0: labelValue  = string.Format("{0}", Step01); labelProperty = "Step 1"; break;
                    case 1: labelValue  = string.Format("{0}", Step02); labelProperty = "Step 2"; break;
                    case 2: labelValue  = string.Format("{0}", Step03); labelProperty = "Step 3"; break;
                    case 3: labelValue  = string.Format("{0}", Step04); labelProperty = "Step 4"; break;
                    case 4: labelValue  = string.Format("{0}", Step05); labelProperty = "Step 5"; break;
                    case 5: labelValue  = string.Format("{0}", Step06); labelProperty = "Step 6"; break;
                    case 6: labelValue  = string.Format("{0}", Step07); labelProperty = "Step 7"; break;
                    case 7: labelValue  = string.Format("{0}", Step08); labelProperty = "Step 8"; break;
                    case 8: labelValue  = string.Format("{0}", Step09); labelProperty = "Step 9"; break;
                    case 9: labelValue  = string.Format("{0}", Step10); labelProperty = "Step 10"; break;
                    case 10: labelValue = string.Format("{0}", Step11); labelProperty = "Step 11"; break;
                    case 11: labelValue = string.Format("{0}", Step12); labelProperty = "Step 12"; break;
                    case 12: labelValue = string.Format("{0}", Step13); labelProperty = "Step 13"; break;
                    case 13: labelValue = string.Format("{0}", Step14); labelProperty = "Step 14"; break;
                    case 14: labelValue = string.Format("{0}", Step15); labelProperty = "Step 15"; break;
                    case 15: labelValue = string.Format("{0}", Step16); labelProperty = "Step 16"; break;
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
