using RentalOfPremises.Services.Contracts.Enums;

namespace RentalOfPremises.Services.Contracts.RequestModels
{
    /// <summary>
    /// Модель запроса арендатора
    /// </summary>
    public class TenantRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Имя директора
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия директора
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Отчество директора
        /// </summary>
        public string? Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Тип организации
        /// </summary>
        public TenantTypes Type { get; set; }

        /// <summary>
        /// ИНН организации
        /// </summary>
        public string Inn { get; set; } = string.Empty;

        /// <summary>
        /// КПП организации
        /// </summary>
        public string? Kpp { get; set; } = string.Empty;

        /// <summary>
        /// Юридический адрес организации
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Номер расчётного счёта
        /// </summary>
        public string Rs { get; set; } = string.Empty;

        /// <summary>
        /// Номер корреспондентского счёта
        /// </summary>
        public string Ks { get; set; } = string.Empty;

        /// <summary>
        /// Банковский идентификационный код (БИК)
        /// </summary>
        public string Bik { get; set; } = string.Empty;

        /// <summary>
        /// Полное наименование банка
        /// </summary>
        public string Bank { get; set; } = string.Empty;

        /// <summary>
        /// Общероссийский классификатор организаций и предприятий
        /// </summary>
        public string Okpo { get; set; } = string.Empty;

        /// <summary>
        /// Основной государственный регистрационный номер
        /// </summary>
        public string Ogrn { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Telephone { get; set; } = string.Empty;

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string? Email { get; set; } = string.Empty;
    }
}
