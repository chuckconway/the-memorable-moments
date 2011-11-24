using System.Web.Mvc;

namespace TheMemorableMoments.UI.Web.Validation
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Validations the hack remove name and blank key.
        /// </summary>
        public static void ValidationHackRemoveNameAndBlankKey(ModelStateDictionary modelStateDictionary)
        {
            ModelState modelState = modelStateDictionary[string.Empty];

            if (modelState != null)
            {
                //Setting the new error with an name. Removeing the old one which doesn't have a name.
                modelStateDictionary.AddModelError("PasswordDontMatch", modelState.Errors[0].ErrorMessage);
                modelStateDictionary.Remove(string.Empty);
            }
        }
    }
}