using RentalOfPremises.Services.Contracts.Enums;

namespace RentalOfPremises.Services.Contracts.Models
{
    /// <summary>
    /// Модель токена
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Тип
        /// </summary>
        public RoleTypes RoleUser { get; set; } = RoleTypes.Employee;

        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
