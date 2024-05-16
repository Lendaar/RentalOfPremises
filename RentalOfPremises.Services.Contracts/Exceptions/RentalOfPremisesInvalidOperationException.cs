namespace RentalOfPremises.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибка выполнения операции
    /// </summary>
    public class RentalOfPremisesInvalidOperationException : RentalOfPremisesException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RentalOfPremisesInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public RentalOfPremisesInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
