using System.Collections.Generic;
using Hypersonic;
using TheMemorableMoments.Domain.Model;
using TheMemorableMoments.Domain.Model.MediaClasses;
using TheMemorableMoments.Infrastructure.Repositories.Services;

namespace TheMemorableMoments.Infrastructure.Repositories
{
   public class MemberRepository : RepositoryBase, IMemberRepository
   {
       private readonly IMediaFileHydrationService _hydrationService;

       /// <summary>
       /// Initializes a new instance of the <see cref="MemberRepository"/> class.
       /// </summary>
       /// <param name="hydrationService">The hydration service.</param>
       public MemberRepository(IMediaFileHydrationService hydrationService)
       {
           _hydrationService = hydrationService;
       }

       /// <summary>
       /// Retrieves the members.
       /// </summary>
       /// <returns></returns>
       public List<Member> RetrieveMembers()
       {
           return database.PopulateCollection("User_RetrieveUserAndMedia", PopulateMember);
       }

       /// <summary>
       /// Populates the member.
       /// </summary>
       /// <param name="reader">The reader.</param>
       /// <returns></returns>
       public Member PopulateMember(INullableReader reader)
       {
           Member member = new Member
                               {
                                   Media = database.AutoPopulate<Media>(reader),
                                   User = UserRepository.Populate(reader)
                               };
           _hydrationService.Populate(new List<Media> { member.Media });
           return member;
       }

    }
}
