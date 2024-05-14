namespace RentalOfPremises.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемый ресурс не найден
    /// </summary>
    public class RentalOfPremisesNotFoundException : RentalOfPremisesException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RentalOfPremisesNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public RentalOfPremisesNotFoundException(string message)
            : base(message)
        { }
    }
}
