using RentalOfPremises.Services.Contracts.Enums;

namespace RentalOfPremises.Services.Contracts.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public RoleTypes RoleUser { get; set; }
    }
}
