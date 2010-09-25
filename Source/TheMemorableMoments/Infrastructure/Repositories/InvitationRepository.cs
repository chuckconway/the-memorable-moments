using Hypersonic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments.Infrastructure.Repositories
{
    public class InvitationRepository : RepositoryBase, IInvitationRepository
    {
        /// <summary>
        /// Adds the specified invitation.
        /// </summary>
        /// <param name="invitation">The invitation.</param>
        public void Add(Invitation invitation)
        {
            foreach (string email in invitation.Email)
            {
                database.NonQuery("Invitation_Insert", new {email, invitation.UserId});
            }
        }

        /// <summary>
        /// Invitationses the left.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public byte InvitationsLeft(int userId)
        {
            return  database.PopulateItem("Invitation_UserInvitationsRemaining", new {userId}, PopulateRemainingCount);
        }

        /// <summary>
        /// Populates the remaining count.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static byte PopulateRemainingCount(INullableReader reader)
        {
            return (byte)reader.GetInt32("RemainingInvitations");
        }
    }
}
