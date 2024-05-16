namespace RentalOfPremises.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class RentalOfPremisesException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RentalOfPremisesException"/> без параметров
        /// </summary>
        protected RentalOfPremisesException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RentalOfPremisesException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected RentalOfPremisesException(string message)
            : base(message) { }
    }
}
