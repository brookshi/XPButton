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
using Windows.UI.Xaml.Data;

namespace XP
{
    public class MarginConverter : DependencyObject, IValueConverter
    {
        public IconPosition IconPosition
        {
            get { return (IconPosition)GetValue(IconPositionProperty); }
            set { SetValue(IconPositionProperty, value); }
        }
        public static readonly DependencyProperty IconPositionProperty =
            DependencyProperty.Register("IconPosition", typeof(IconPosition), typeof(MarginConverter), new PropertyMetadata(IconPosition.Left));


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (IconPosition)
            {
                case IconPosition.Left:
                    return new Thickness(double.Parse(value.ToString()), 0, 0, 0);
                case IconPosition.Right:
                    return new Thickness(0, 0, double.Parse(value.ToString()), 0);
                case IconPosition.Top:
                    return new Thickness(0, double.Parse(value.ToString()), 0, 0);
                case IconPosition.Bottom:
                    return new Thickness(0, 0, 0, double.Parse(value.ToString()));
                default:
                    return new Thickness(0, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
