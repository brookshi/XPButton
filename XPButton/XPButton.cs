using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
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

        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register("ShadowColor", typeof(Brush), typeof(XPButton), new PropertyMetadata(Colors.Black));

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
                var border = new Border() { Background = new SolidColorBrush(Colors.Transparent) };
                _shadowBorders.Add(border);
                _shadowGrid.Children.Add(border);
            }
            UpdateShadowEffect();
        }

        void UpdateShadowEffect()
        {
            if (ShadowBorderCount < 1)
                return;

            var lenGap = ShadowLength / ShadowBorderCount ;
            var alphaArr =  ShadowCalculation.GetShadowValues(ShadowBorderCount);//BuildShadowAlpha();//
            var width = Math.Max(0, ActualWidth - ShadowLength * 2 );
            var height = Math.Max(0, ActualHeight - ShadowLength * 2);
            int index = 0;
           
            _shadowBorders.ForEach(o =>
            {
                o.Width = ActualWidth - 2 * lenGap * index;
                o.Height = ActualHeight - 2 * lenGap * index;
                o.BorderThickness = new Thickness(ShadowLength - lenGap *  index);
                o.CornerRadius = new CornerRadius(GetCornerRadius(o.Width));
                o.BorderBrush = new SolidColorBrush(Color.FromArgb((byte)alphaArr[ShadowBorderCount-index-1], ShadowColor.R, ShadowColor.G, ShadowColor.B));
                index++;
            });
            _contentPresenter.Width = width + lenGap;// + ShadowLength;
            _contentPresenter.Height = height + lenGap;// + ShadowLength;
            _contentPresenter.Margin = new Thickness(0, 0, lenGap/2, lenGap/2);
            _backgroundBorder.Width = width + lenGap;// + ShadowLength;
            _backgroundBorder.Height = height + lenGap;// + ShadowLength;
            _backgroundBorder.Margin = new Thickness(0, 0, lenGap / 2, lenGap / 2);
            _backgroundBorder.CornerRadius = new CornerRadius(GetCornerRadius(_backgroundBorder.Width));
        }

        double GetCornerRadius(double width)
        {
            return CornerRadius < 1 && CornerRadius > 0 ? width / 2 : CornerRadius * width / ActualWidth;
        }

        byte[] BuildShadowAlpha()
        {
            //int maxAlpha = 150;
            //int minAplha = 10;
            //int gap = (maxAlpha - minAplha) / (ShadowBorderCount - 1);
            //int[] alphaArray = new int[ShadowBorderCount];
            
            //for(int i=0;i<ShadowBorderCount;i++)
            //{
            //    alphaArray[i] = gap;
            //}
            //alphaArray[alphaArray.Length - 1] = minAplha;
            //return alphaArray;

            byte[] alphaArray = new byte[ShadowBorderCount];
            alphaArray[0] = 60;
            for (int i = 0; i < ShadowBorderCount - 1; i++)
            {
                alphaArray[i + 1] = (byte)Math.Max(2, alphaArray[i] - Math.Max(20 - 5 * i, 2));
            }
            return alphaArray;
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
