using RentalOfPremises.Context.Contracts.Enums;

namespace RentalOfPremises.Context.Contracts.Models
{
    /// <summary>
    /// Сущность помещения
    /// </summary>
    public class Room : BaseAuditEntity
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
        public PremisesTypes TypeRoom { get; set; }

        /// <summary>
        /// Занято ли помещение
        /// </summary>
        public bool Occupied { get; set; }

        public ICollection<Contract> Contract { get; set; }
    }
}
