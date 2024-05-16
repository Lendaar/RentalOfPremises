﻿using System.Diagnostics.CodeAnalysis;

namespace RentalOfPremises.Repositories.Contracts
{
    /// <summary>
    /// Интерфейс создания и модификации записей в хранилище
    /// </summary>
    public interface IRepositoryWriter<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity, string createdBy = "");

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update([NotNull] TEntity entity, string updateBy = "");

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity, string updateBy = "");
    }
}
