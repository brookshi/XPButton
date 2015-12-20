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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace XP
{
    public class XPButtonCommon
    {
        #region adjust content

        public void HorizontalCenterElements(ButtonBase btn, Viewbox symbolView, ContentPresenter contentPresenter, IconPosition iconPos, double iconInterval)
        {
            if (symbolView == null || contentPresenter == null)
                return;

            switch (iconPos)
            {
                case IconPosition.Left:
                    symbolView.Margin = new Thickness(CalculateMarginWidth(btn, symbolView, contentPresenter, iconInterval), 0, 0, 0);
                    break;
                case IconPosition.Right:
                    symbolView.Margin = new Thickness(0, 0, CalculateMarginWidth(btn, symbolView, contentPresenter, iconInterval), 0);
                    break;
                case IconPosition.Top:
                    symbolView.Margin = new Thickness(0, CalculateMarginHeight(btn, symbolView, contentPresenter, iconInterval), 0, 0);
                    break;
                case IconPosition.Bottom:
                    symbolView.Margin = new Thickness(0, 0, 0, CalculateMarginHeight(btn, symbolView, contentPresenter, iconInterval));
                    break;
            }
        }

        double CalculateMarginWidth(ButtonBase btn, Viewbox symbolView, ContentPresenter contentPresenter, double iconInterval)
        {
            var buttonPaddingWidth = btn.Padding.Left + btn.Padding.Right;
            var marginWidth = (btn.ActualWidth - iconInterval - symbolView.DesiredSize.Width - contentPresenter.DesiredSize.Width - buttonPaddingWidth) / 2;
            return Math.Max(0, marginWidth);
        }

        double CalculateMarginHeight(ButtonBase btn, Viewbox symbolView, ContentPresenter contentPresenter, double iconInterval)
        {
            var buttonPaddingHeight = btn.Padding.Top + btn.Padding.Bottom;
            var marginHeight = (btn.ActualHeight - iconInterval - symbolView.DesiredSize.Height - contentPresenter.DesiredSize.Height - buttonPaddingHeight) / 2;
            return Math.Max(0, marginHeight);
        }

        #endregion
    }
}
