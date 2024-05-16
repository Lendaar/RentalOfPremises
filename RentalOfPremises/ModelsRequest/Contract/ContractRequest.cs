namespace RentalOfPremises.Api.ModelsRequest.Contract
{
    public class ContractRequest : CreateContractRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
