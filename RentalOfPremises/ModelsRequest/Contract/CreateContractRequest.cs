namespace RentalOfPremises.Api.ModelsRequest.Contract
{
    /// <summary>
    /// Модель запроса создания Договора
    /// </summary>
    public class CreateContractRequest
    {
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
