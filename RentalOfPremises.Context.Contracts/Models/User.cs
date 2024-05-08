using RentalOfPremises.Context.Contracts.Enums;

namespace RentalOfPremises.Context.Contracts.Models
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User : BaseAuditEntity
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
        public RoleTypes RoleUser { get; set; }
    }
}
