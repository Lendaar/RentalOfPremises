namespace RentalOfPremises.Api.ModelsRequest.Tenant
{
    public class TenantRequest : CreateTenantRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
