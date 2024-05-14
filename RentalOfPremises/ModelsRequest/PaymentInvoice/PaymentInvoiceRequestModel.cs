namespace RentalOfPremises.Api.ModelsRequest.PaymentInvoice
{
    public class PaymentInvoiceRequest : CreatePaymentInvoiceRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
