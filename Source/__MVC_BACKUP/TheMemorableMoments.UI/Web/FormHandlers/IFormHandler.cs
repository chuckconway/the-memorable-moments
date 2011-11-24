using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheMemorableMoments.UI.Web.FormHandlers
{
    public interface IFormHandler<in T>
    {
        /// <summary>
        /// Handles the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        void Handle(T form);
    }
}