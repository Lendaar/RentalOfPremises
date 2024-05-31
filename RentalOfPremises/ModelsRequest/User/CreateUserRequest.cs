using RentalOfPremises.Api.Enums;

namespace RentalOfPremises.Api.ModelsRequest.User
{
    /// <summary>
    /// Модель запроса создания пользователя
    /// </summary>
    public class CreateUserRequest
    {
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
