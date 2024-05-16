using RentalOfPremises.Api.Enums;

namespace RentalOfPremises.Api.ModelsRequest.Room
{
    /// <summary>
    /// Модель запроса создания помещения
    /// </summary>
    public class CreateRoomRequest
    {
        /// <summary>
        /// Литер
        /// </summary>
        public string Liter { get; set; } = string.Empty;

        /// <summary>
        /// Номер помещения
        /// </summary>
        public int NumberRoom { get; set; }

        /// <summary>
        /// Площадь
        /// </summary>
        public double SquareRoom { get; set; }

        /// <summary>
        /// Тип помещения
        /// </summary>
        public PremisesTypes TypeRoom { get; set; } = PremisesTypes.OwnNonResidentialPremises;

        /// <summary>
        /// Занято ли помещение
        /// </summary>
        public bool Occupied { get; set; } = false;
    }
}
