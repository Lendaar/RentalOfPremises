using RentalOfPremises.Shared;

namespace RentalOfPremises.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class RentalOfPremisesValidationException : RentalOfPremisesException
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RentalOfPremisesValidationException"/>
        /// </summary>
        public RentalOfPremisesValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
