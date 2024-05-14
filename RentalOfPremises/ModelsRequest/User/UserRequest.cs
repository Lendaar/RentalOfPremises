namespace RentalOfPremises.Api.ModelsRequest.User
{
    public class UserRequest : CreateUserRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
