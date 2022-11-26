using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Papara.Core.Entites;
using Papara.Core.Enums;
using Papara.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Infrastructure.Repository
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly DbSet<Member> _members;
        public MemberRepository(ApplicationDbContext dbContext, Func<CacheTech, ICacheService> cacheService) : base(dbContext, cacheService)
        {
            _members = dbContext.Set<Member>();
        }
    }
}
