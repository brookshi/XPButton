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

using Windows.UI.Xaml;

namespace XP
{
    public class IconPositionTrigger : StateTriggerBase
    {
        public IconPosition TriggerIconPosition { get; set; }

        public IconPosition ButtonIconPosition
        {
            get { return (IconPosition)GetValue(ButtonIconPositionProperty); }
            set { SetValue(ButtonIconPositionProperty, value); }
        }
        public static readonly DependencyProperty ButtonIconPositionProperty =
                DependencyProperty.Register("ButtonIconPosition", typeof(IconPosition), typeof(IconPositionTrigger), new PropertyMetadata(IconPosition.Left, (s, e)=> {
                var trigger = s as IconPositionTrigger;
                if (trigger == null)
                    return;

                trigger.SetActive(trigger.TriggerIconPosition == (IconPosition)e.NewValue);
            }));
    }
}
