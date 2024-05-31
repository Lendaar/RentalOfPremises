namespace RentalOfPremises.Services.Contracts.RequestModels
{
    /// <summary>
    /// Модель запроса счета на оплату
    /// </summary>
    public class PaymentInvoiceRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер договора
        /// </summary>
        public int NumberContract { get; set; }

        /// <summary>
        /// Период оплаты
        /// </summary>
        public int PeriodPayment { get; set; }

        /// <summary>
        /// Потребление электричества
        /// </summary>
        public int Electricity { get; set; }

        /// <summary>
        /// Потребление воды
        /// </summary>
        public int WaterPl { get; set; }

        /// <summary>
        /// Водоотведение
        /// </summary>
        public int WaterMi { get; set; }

        /// <summary>
        /// Кол-во пропусков на человека
        /// </summary>
        public int PassPerson { get; set; }

        /// <summary>
        /// Кол-во пропусков на легковую машину
        /// </summary>
        public int? PassLegСar { get; set; }

        /// <summary>
        /// Кол-во пропусков на грузовую машину
        /// </summary>
        public int? PassGrСar { get; set; }

        /// <summary>
        /// Идентификатор прейскуранта
        /// </summary>
        public Guid Price { get; set; }
    }
}
