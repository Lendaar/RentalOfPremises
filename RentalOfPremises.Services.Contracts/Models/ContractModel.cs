namespace RentalOfPremises.Services.Contracts.Models
{
    /// <summary>
    /// Модель Договора
    /// </summary>
    public class ContractModel
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
        public TenantModel Tenant { get; set; }

        /// <summary>
        /// Идентификатор помещения
        /// </summary>
        public RoomModel Room { get; set; }

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
