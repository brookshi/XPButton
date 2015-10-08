using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace XP
{
    public sealed class XPButton : Button
    {
        private Grid _shadowGrid;
        private ContentPresenter _contentPresenter;
        private Border _backgroundBorder;
        private List<Border> _shadowBorders = new List<Border>();


        public double ShadowLength
        {
            get { return (double)GetValue(ShadowLengthProperty); }
            set { SetValue(ShadowLengthProperty, value); }
        }
        public static readonly DependencyProperty ShadowLengthProperty =
            DependencyProperty.Register("ShadowLength", typeof(double), typeof(XPButton), new PropertyMetadata(0));

        public int ShadowBorderCount
        {
            get { return (int)GetValue(ShadowBorderCountProperty); }
            set { SetValue(ShadowBorderCountProperty, value); }
        }
        public static readonly DependencyProperty ShadowBorderCountProperty =
            DependencyProperty.Register("ShadowBorderCount", typeof(int), typeof(XPButton), new PropertyMetadata(0));

        public Brush ShadowBrush
        {
            get { return (Brush)GetValue(ShadowBrushProperty); }
            set { SetValue(ShadowBrushProperty, value); }
        }
        public static readonly DependencyProperty ShadowBrushProperty =
            DependencyProperty.Register("ShadowBrush", typeof(Brush), typeof(XPButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(XPButton), new PropertyMetadata(0));




        public XPButton()
        {
            this.DefaultStyleKey = typeof(XPButton);
            Loaded += XPButton_Loaded;
            SizeChanged += XPButton_SizeChanged;
        }

        private void XPButton_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateShadowEffect();
        }

        private void XPButton_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ShadowBorderCount; i++)
            {
                var border = new Border() { Background = new SolidColorBrush(Colors.Transparent), BorderBrush = ShadowBrush };
                _shadowBorders.Add(border);
                _shadowGrid.Children.Add(border);
            }
            UpdateShadowEffect();
        }

        void UpdateShadowEffect()
        {
            var lenGap = ShadowLength / ShadowBorderCount;
            var opacityGap = 0.16 / ShadowBorderCount;
            var width = Math.Max(0, ActualWidth - ShadowLength);
            var height = Math.Max(0, ActualHeight - ShadowLength);
            int index = 0;

            _shadowBorders.ForEach(o =>
            {
                o.Width = ActualWidth - lenGap * index;
                o.Height = ActualHeight - lenGap * index;
                Debug.WriteLine("shadow " + index.ToString() + " height : " + o.Height.ToString());
                o.BorderThickness = new Thickness(lenGap);
                o.CornerRadius = new CornerRadius(CornerRadius < 1 ? o.Width / 2 : CornerRadius);
                o.Opacity = opacityGap * (index + 1);
                index++;
            });

            _contentPresenter.Width = width;
            _contentPresenter.Height = height;
            _backgroundBorder.Width = width;
            _backgroundBorder.Height = height;
            Debug.WriteLine("background border height : " + height.ToString());
            _backgroundBorder.CornerRadius = new CornerRadius(CornerRadius < 1 ? width / 2 : CornerRadius);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _shadowGrid = (Grid)GetTemplateChild("ShadowGrid");
            _contentPresenter = (ContentPresenter)GetTemplateChild("ContentPresenter");
            _backgroundBorder = (Border)GetTemplateChild("BackgroundBorder");
        }
    }
}
