namespace RentalOfPremises.Api.ModelsRequest.Price
{
    public class PriceRequest : CreatePriceRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
