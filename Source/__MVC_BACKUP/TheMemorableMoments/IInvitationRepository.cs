using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments
{
    public interface IInvitationRepository
    {
        /// <summary>
        /// Adds the specified invitation.
        /// </summary>
        /// <param name="invitation">The invitation.</param>
        void Add(Invitation invitation);

        /// <summary>
        /// Invitationses the left.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        byte InvitationsLeft(int userId);
    }
}