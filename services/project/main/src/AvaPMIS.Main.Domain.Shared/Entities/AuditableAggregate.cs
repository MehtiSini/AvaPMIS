using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace AvaPMIS.Main.Entities
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObject"/> for aggregate roots.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditableAggregate<TKey> : AuditedAggregateRoot<TKey>, IDeletionAuditedObject
        
    {
        public AuditableAggregate():base(){}
        public AuditableAggregate(TKey key):base(key){}
        /// <inheritdoc />
        public Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public DateTime? DeletionTime { get; set; }

        /// <inheritdoc />
        public bool IsDeleted { get; set; }
    }
}
