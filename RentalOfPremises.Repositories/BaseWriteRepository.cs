using System.Diagnostics.CodeAnalysis;
using RentalOfPremises.Common.Entity;
using RentalOfPremises.Common.Entity.EntityInterface;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Repositories.Contracts;

namespace RentalOfPremises.Repositories
{
    /// <summary>
    /// Базовый класс репозитория записи данных
    /// </summary>
    public abstract class BaseWriteRepository<T> : IRepositoryWriter<T> where T : class, IEntity
    {
        /// <inheritdoc cref="IDbWriterContext"/>
        private readonly IDbWriterContext writerContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BaseWriteRepository{T}"/>
        /// </summary>
        protected BaseWriteRepository(IDbWriterContext writerContext)
        {
            this.writerContext = writerContext;
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public virtual void Add([NotNull] T entity, string createdBy = "")
        {
            if (entity is IEntityWithId entityWithId &&
                entityWithId.Id == Guid.Empty)
            {
                entityWithId.Id = Guid.NewGuid();
            }
            AuditForCreate(entity, createdBy);
            AuditForUpdate(entity, createdBy);
            writerContext.Writer.Add(entity);
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public void Update([NotNull] T entity, string updatedBy = "")
        {
            AuditForUpdate(entity, updatedBy);
            writerContext.Writer.Update(entity);
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public void Delete([NotNull] T entity, string updatedBy = "")
        {
            AuditForUpdate(entity, updatedBy);
            AuditForDelete(entity);
            if (entity is IEntityAuditDeleted)
            {
                writerContext.Writer.Update(entity);
            }
            else
            {
                writerContext.Writer.Delete(entity);
            }
        }

        private void AuditForCreate([NotNull] T entity, string createdBy)
        {
            if (entity is IEntityAuditCreated auditCreated)
            {
                auditCreated.CreatedAt = writerContext.DateTimeProvider.UtcNow;
                auditCreated.CreatedBy = createdBy == null ? writerContext.UserName : createdBy;
            }
        }

        private void AuditForUpdate([NotNull] T entity, string updateBy)
        {
            if (entity is IEntityAuditUpdated auditUpdate)
            {
                auditUpdate.UpdatedAt = writerContext.DateTimeProvider.UtcNow;
                auditUpdate.UpdatedBy = updateBy == null ? writerContext.UserName : updateBy;
            }
        }

        private void AuditForDelete([NotNull] T entity)
        {
            if (entity is IEntityAuditDeleted auditDeleted)
            {
                auditDeleted.DeletedAt = writerContext.DateTimeProvider.UtcNow;
            }
        }
    }
}
