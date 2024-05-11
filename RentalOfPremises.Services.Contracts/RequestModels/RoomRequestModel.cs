using RentalOfPremises.Services.Contracts.Enums;

namespace RentalOfPremises.Services.Contracts.RequestModels
{
    /// <summary>
    /// Модель запроса помещения
    /// </summary>
    public class RoomRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public PremisesTypes TypeRoom { get; set; }

        /// <summary>
        /// Цена аренды
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Занято ли помещение
        /// </summary>
        public bool Occupied { get; set; } = false;
    }
}
