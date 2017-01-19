using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renault.Interfaces
{
    /// <summary>
    /// Toast notifiation interface.
    /// </summary>
    public interface IToastNotifier
    {
        /// <summary>
        /// Shows message in current page. 
        /// </summary>
        /// <param name="type">type of message</param>
        /// <param name="title">message title</param>
        /// <param name="description">message description</param>
        /// <param name="duration">time duration</param>
        /// <param name="context">context</param>
        /// <returns></returns>
        Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null);
        /// <summary>
        /// 
        /// </summary>
        void HideAll();
    }
    /// <summary>
    /// Toast notification type.
    /// </summary>
    public enum ToastNotificationType
    {
        Info,
        Success,
        Error,
        Warning
    }
}
