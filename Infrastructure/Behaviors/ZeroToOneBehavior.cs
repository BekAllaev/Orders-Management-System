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
            //AssociatedObject.Initialized += AssociatedObject_Initialized;
            AssociatedObject.ValueChanged += AssociatedObject_ValueChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Initialized -= AssociatedObject_Initialized;
            AssociatedObject.ValueChanged -= AssociatedObject_ValueChanged;
        }

        private void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            UpDown upDown = (UpDown)sender;
            upDown.Value = 1;
        }

        private void AssociatedObject_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetToOne((UpDown)d);
        }

        private void SetToOne(UpDown upDown)
        {
            if (upDown.Value == 0) upDown.Value = 1;
        }
    }
}
