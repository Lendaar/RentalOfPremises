﻿namespace RentalOfPremises.Services.Contracts.RequestModels
{
    /// <summary>
    /// Модель запроса Договора
    /// </summary>
    public class ContractRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер договора
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Платеж
        /// </summary>
        public decimal Payment { get; set; }

        /// <summary>
        /// Идентификатор арендатора
        /// </summary>
        public Guid Tenant { get; set; }

        /// <summary>
        /// Идентификатор помещения
        /// </summary>
        public Guid Room { get; set; }

        /// <summary>
        /// Дата начала действия договора
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Дата окончания действия договора
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Архивный ли договор
        /// </summary>
        public bool Archive { get; set; } = false;
    }
}