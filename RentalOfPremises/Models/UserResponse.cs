namespace RentalOfPremises.Api.Models
{
    /// <summary>
    /// Модель ответа пользователя
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string LoginUser { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string PasswordUser { get; set; } = string.Empty;

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string RoleUser { get; set; } = string.Empty;
    }
}
