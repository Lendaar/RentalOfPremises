using RentalOfPremises.Api.Enums;

namespace RentalOfPremises.Api.Models
{
    /// <summary>
    /// Модель ответа токена
    /// </summary>
    public class TokenResponse
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
