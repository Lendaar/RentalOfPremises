namespace RentalOfPremises.Api.ModelsRequest.PaymentInvoice
{
    /// <summary>
    /// Модель запроса создания счета на оплату
    /// </summary>
    public class CreatePaymentInvoiceRequest
    {
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
        public int PassLegСar { get; set; }

        /// <summary>
        /// Кол-во пропусков на грузовую машину
        /// </summary>
        public int PassGrСar { get; set; }
    }
}
