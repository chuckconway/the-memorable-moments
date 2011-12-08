namespace Momntz.UI.Web.Forms
{
    public interface IFormHandler<in T>
    {
        void Handle(T form);
    }
}