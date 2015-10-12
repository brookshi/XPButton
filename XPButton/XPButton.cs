#region License
//   Copyright 2015 Brook Shi
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            for (int i = 0; i < GetBorderCount(); i++)
            {
                var border = new Border() { Background = new SolidColorBrush(Colors.Transparent) };
                _shadowBorders.Add(border);
                _shadowGrid.Children.Add(border);
            }
            UpdateShadowEffect();
        }

        int GetBorderCount()
        {
            return (int)(GetResolutionScale() * ShadowLength + 0.5);
        }

        double GetResolutionScale()
        {
            return (double)DisplayInformation.GetForCurrentView().ResolutionScale / 100;
        }

        void UpdateShadowEffect()
        {
            if (ShadowBorderCount > 0)
            {
                var scale = GetResolutionScale();
                var count = GetBorderCount();
                var lenGap = ShadowLength / count;
                var alphaArr = ShadowCalculation.GetShadowValues(count);
                var width = Math.Max(0, ActualWidth - ShadowLength * 2);
                var height = Math.Max(0, ActualHeight - ShadowLength * 2);
                int index = 0;

                _shadowBorders.ForEach(o =>
                {
                    o.Width = ActualWidth - 2 / scale * index;
                    o.Height = ActualHeight - 2 / scale * index;
                    o.BorderThickness = new Thickness(1 / scale);
                    o.CornerRadius = new CornerRadius(GetCornerRadius(o.Width));
                    o.BorderBrush = new SolidColorBrush(Color.FromArgb((byte)alphaArr[count - index - 1], ShadowColor.R, ShadowColor.G, ShadowColor.B));
                    index++;
                });
                _contentPresenter.Width = width + lenGap;
                _contentPresenter.Height = height + lenGap;
                _contentPresenter.Margin = new Thickness(0, 0, lenGap / 2.1, lenGap / 2.1);
                _backgroundBorder.Width = width + lenGap;
                _backgroundBorder.Height = height + lenGap;
                _backgroundBorder.Margin = new Thickness(0, 0, lenGap / 2.1, lenGap / 2.1);
            }

            _backgroundBorder.CornerRadius = new CornerRadius(GetCornerRadius(_backgroundBorder.Width));
        }

        double GetCornerRadius(double width)
        {
            return CornerRadius < 1 && CornerRadius > 0 ? width / 2 : CornerRadius * width / ActualWidth;
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
