using System.Collections.Generic;
using TheMemorableMoments.Domain.Model;

namespace TheMemorableMoments
{
    public interface IMemberRepository
    {
        List<Member> RetrieveMembers();
    }
}