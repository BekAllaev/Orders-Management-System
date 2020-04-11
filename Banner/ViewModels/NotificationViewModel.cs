using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Infrastructure.Events;
using System.Data.Common;
using System.Data.Entity.Core;

namespace Banner.ViewModels
{
    public class NotificationViewModel : ReactiveObject
    {
        #region Declarations
        private string _notificationString;
        #endregion

        #region Constructors
        public NotificationViewModel()
        {
            MessageBus.Current.Listen<DbException>().
                Subscribe(exception =>
                NotificationString = "Message: " + exception.Message);

            MessageBus.Current.Listen<EntityCommandExecutionException>().
                Subscribe(exception => NotificationString = "Message: " + exception.Message);
        }
        #endregion

        #region Properties
        public string NotificationString
        {
            set { this.RaiseAndSetIfChanged(ref _notificationString, value); }
            get { return _notificationString; }
        }
        #endregion
    }
}
