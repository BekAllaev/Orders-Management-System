using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using Syncfusion.Windows.Shared;

namespace Infrastructure.Behaviors
{
    public class ZeroToOneBehavior : Behavior<UpDown>
    {
        protected override void OnAttached()
        {
            AssociatedObject.ValueChanged += AssociatedObject_ValueChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.ValueChanged -= AssociatedObject_ValueChanged;
        }

        private void AssociatedObject_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpDown upDown = (UpDown)d;
            if (upDown.Value == 0) upDown.Value = 1;
        }
    }
}
