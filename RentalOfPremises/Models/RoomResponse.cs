namespace RentalOfPremises.Api.Models
{
    /// <summary>
    /// Модель ответа помещения
    /// </summary>
    public class RoomResponse
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
        public string TypeRoom { get; set; } = string.Empty;

        /// <summary>
        /// Цена аренды
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Занято ли помещение
        /// </summary>
        public bool Occupied { get; set; }
    }
}
