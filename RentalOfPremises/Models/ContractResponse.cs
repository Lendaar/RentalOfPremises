namespace RentalOfPremises.Api.Models
{
    /// <summary>
    /// Модель ответа Договора
    /// </summary>
    public class ContractResponse
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
        /// Арендатор
        /// </summary>
        public string TenantTitle { get; set; }

        /// <summary>
        /// Инн арендатора
        /// </summary>
        public string TenantInn { get; set; }

        /// <summary>
        /// Литер помещения
        /// </summary>
        public string RoomLiter { get; set; }

        /// <summary>
        /// Номер помещения
        /// </summary>
        public string RoomNumber { get; set; }

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
        public bool Archive { get; set; }
    }
}
