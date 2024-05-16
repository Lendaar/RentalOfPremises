namespace RentalOfPremises.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемая сущность не найдена
    /// </summary>
    public class RentalOfPremisesEntityNotFoundException<TEntity> : RentalOfPremisesNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RentalOfPremisesEntityNotFoundException{TEntity}"/>
        /// </summary>
        public RentalOfPremisesEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
