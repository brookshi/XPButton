using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
