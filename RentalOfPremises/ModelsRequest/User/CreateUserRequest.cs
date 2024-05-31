using RentalOfPremises.Api.Enums;

namespace RentalOfPremises.Api.ModelsRequest.User
{
    /// <summary>
    /// Модель запроса создания пользователя
    /// </summary>
    public class CreateUserRequest
    {
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
        public RoleTypes RoleUser { get; set; } = RoleTypes.Employee;
    }
}
