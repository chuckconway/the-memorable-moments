using Momntz.Commands;
using Momntz.Exceptions;
using Momntz.Infrastructure;
using Momntz.UI.Models.Signup;

namespace Momntz.UI.Web.Forms
{
    public class SignupHandler : IFormHandler<SignupView>
    {
        private readonly ICommandProcessor _commandProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignupHandler"/> class.
        /// </summary>
        /// <param name="commandProcessor">The command processor.</param>
        public SignupHandler(ICommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        /// <summary>
        /// Handles the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        public void Handle(SignupView form)
        {
            CreateUserCommand createUser = new CreateUserCommand(form.FirstName, form.LastName, form.Username, form.Password, form.Email);
            _commandProcessor.Process(createUser);
        }
    }
}