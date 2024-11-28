using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.EvokeControls
{
    public class ThreeDotsLoader : Control
    {
        static ThreeDotsLoader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThreeDotsLoader), new FrameworkPropertyMetadata(typeof(ThreeDotsLoader)));
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(ThreeDotsLoader), new PropertyMetadata(false));

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(ThreeDotsLoader), new PropertyMetadata(Brushes.LightBlue));
        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(ThreeDotsLoader), new PropertyMetadata(1.0));
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(ThreeDotsLoader), new PropertyMetadata(default(Duration), CalculateSpeedRatio));
        private static void CalculateSpeedRatio(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (d is ThreeDotsLoader control)
                {
                    double spr = 1.0 / control.Duration.TimeSpan.TotalSeconds;
                    control.SpeedRatio = spr;
                }
            }
            catch
            {
                if (d is ThreeDotsLoader control)
                    control.SpeedRatio = 1.0;
            }
        }
    }
}
