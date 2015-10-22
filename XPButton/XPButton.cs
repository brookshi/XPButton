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
using System.Numerics;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using System.Diagnostics;

namespace XP
{
    public sealed class XPButton : Button
    {
        private SymbolIcon _symbol;
        private Viewbox _symbolView;
        private RelativePanel _visualPanel;
        private ContentPresenter _contentPresenter;

        public Symbol Icon
        {
            get { return (Symbol)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Symbol), typeof(XPButton), new PropertyMetadata(null));

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(XPButton), new PropertyMetadata(20));

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(XPButton), new PropertyMetadata(new Thickness(5, 0, 5, 0)));

        public IconPosition IconPosition
        {
            get { return (IconPosition)GetValue(IconPositionProperty); }
            set { SetValue(IconPositionProperty, value); }
        }
        public static readonly DependencyProperty IconPositionProperty =
            DependencyProperty.Register("IconPosition", typeof(IconPosition), typeof(XPButton), new PropertyMetadata(IconPosition.Left));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(XPButton), new PropertyMetadata(new CornerRadius(0)));

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(XPButton), new PropertyMetadata(new Thickness(0)));



        protected override Size ArrangeOverride(Size finalSize)
        {
            AdjustContentPresenter(finalSize);
            return base.ArrangeOverride(finalSize);
        }

        #region adjust content

        void AdjustContentPresenter(Size finalSize)
        {
            switch(IconPosition)
            {
                case IconPosition.Left:
                    AdjustContentPresenterForLeft(finalSize);
                    break;
                case IconPosition.Right:
                    AdjustContentPresenterForRight(finalSize);
                    break;
            }
        }

        void AdjustContentPresenterForLeft(Size finalSize)
        {
            if (finalSize.Width < GetDesiredWidth())
            {
                RelativePanel.SetRightOf(_contentPresenter, "SymbolView");
                RelativePanel.SetAlignHorizontalCenterWithPanel(_contentPresenter, false);
            }
            else
            {
                RelativePanel.SetRightOf(_contentPresenter, "");
                RelativePanel.SetAlignHorizontalCenterWithPanel(_contentPresenter, true);
            }
        }

        void AdjustContentPresenterForRight(Size finalSize)
        {
            if (finalSize.Width < GetDesiredWidth())
            {
                RelativePanel.SetLeftOf(_contentPresenter, "SymbolView");
                RelativePanel.SetAlignHorizontalCenterWithPanel(_contentPresenter, false);
            }
            else
            {
                RelativePanel.SetLeftOf(_contentPresenter, "");
                RelativePanel.SetAlignHorizontalCenterWithPanel(_contentPresenter, true);
            }
        }

        double GetDesiredWidth()
        {
            var buttonPaddingWidth = Padding.Left + Padding.Right;
            var iconMarginWidth = IconMargin.Left + IconMargin.Right;

            return buttonPaddingWidth + iconMarginWidth + _symbolView.DesiredSize.Width + _contentPresenter.DesiredSize.Width;
        }

        #endregion

        public XPButton()
        {
            this.DefaultStyleKey = typeof(XPButton);
            Loaded += XPButton_Loaded;
        }

        private void XPButton_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _visualPanel = (RelativePanel)GetTemplateChild("VisualPanel");
            _symbol = (SymbolIcon)GetTemplateChild("Symbol");
            _symbolView = (Viewbox)GetTemplateChild("SymbolView");
            _contentPresenter = (ContentPresenter)GetTemplateChild("ContentPresenter");
        }
    }
}
