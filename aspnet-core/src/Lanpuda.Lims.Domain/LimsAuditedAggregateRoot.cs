using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.Lims
{
    public class LimsAuditedAggregateRoot<TKey> : AuditedAggregateRoot<TKey>
    {
        public IdentityUser? Creator { get; set; }
        public IdentityUser? LastModifier { get; set; }

        protected LimsAuditedAggregateRoot()
        {

        }

        protected LimsAuditedAggregateRoot(TKey id)
            : base(id)
        {

        }

    }
}
