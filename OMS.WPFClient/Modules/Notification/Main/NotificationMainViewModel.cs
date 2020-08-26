using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Net.Http;

namespace OMS.WPFClient.Modules.Notification.Main
{
    public class NotificationMainViewModel : ReactiveObject
    {
        #region Declarations
        private string _notificationString;
        #endregion

        #region Constructors
        public NotificationMainViewModel()
        {
            MessageBus.Current.Listen<DbException>().
                Subscribe(exception => NotificationString = exception.Message + $"Код ошибки: {exception.ErrorCode}.");

            MessageBus.Current.Listen<HttpRequestException>().
                Subscribe(exception => 
                NotificationString = exception.Message + exception.InnerException.Message + ". Проверьте работает ли сервер и перезагрузите приложения. ");

            MessageBus.Current.Listen<EntityException>().
                Subscribe(exception => NotificationString = exception.Message);
        }
        #endregion

        #region Properties
        public string NotificationString
        {
            set { this.RaiseAndSetIfChanged(ref _notificationString, value + " Application will be closed automatically."); }
            get { return _notificationString; }
        }
        #endregion
    }
}
