namespace RentalOfPremises.Services.Contracts.Models
{
    /// <summary>
    /// Модель прейскуранта
    /// </summary>
    public class PriceModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Цена на электричество
        /// </summary>
        public decimal Electricity { get; set; }

        /// <summary>
        /// Цена на водопотребление
        /// </summary>
        public decimal WaterPl { get; set; }

        /// <summary>
        /// Цена на водоотведение
        /// </summary>
        public decimal WaterMi { get; set; }

        /// <summary>
        /// Цена на пропуск на человека
        /// </summary>
        public decimal PassPerson { get; set; }

        /// <summary>
        /// Цена на пропуск на легковой автомобиль
        /// </summary>
        public decimal PassLegСar { get; set; }

        /// <summary>
        /// Цена на пропуск на грузовой автомобиль
        /// </summary>
        public decimal PassGrСar { get; set; }
    }
}
