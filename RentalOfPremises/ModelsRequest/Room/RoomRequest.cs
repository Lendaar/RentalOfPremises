namespace RentalOfPremises.Api.ModelsRequest.Room
{
    public class RoomRequest : CreateRoomRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
