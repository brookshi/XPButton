using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace XP
{
    public class IconPositionTrigger : StateTriggerBase
    {
        private XPButton _button;
        private IconPosition _iconPosition;

        public XPButton Target
        {
            get { return _button; }
            set
            {
                _button = value;
                _button.IconPositionTrigger = iconPosition =>
                {
                    IconPosition = iconPosition;
                };
            }
        }

        public IconPosition IconPosition
        {
            get { return _iconPosition; }
            set
            {
                if(_iconPosition != value)
                {
                    SetActive(true);
                    _iconPosition = value;
                }
            }
        }
    }
}
