namespace RentalOfPremises.Context.Contracts.Models
{
    /// <summary>
    /// Сущность Договора
    /// </summary>
    public class Contract : BaseAuditEntity
    {
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
        public Guid TenantId { get; set; }

        /// <summary>
        /// Сущность арендатора
        /// </summary>
        public Tenant Tenant { get; set; }

        /// <summary>
        /// Идентификатор помещения
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Сущность помещения
        /// </summary>
        public Room Room { get; set; }

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
