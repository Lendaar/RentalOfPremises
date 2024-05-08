namespace RentalOfPremises.Context.Contracts.Models
{
    /// <summary>
    /// Сущность арендатора
    /// </summary>
    public class Tenant : BaseAuditEntity
    {
        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Имя директора
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Фамилия директора
        /// </summary>
        public string Surname { get; set; } = null!;

        /// <summary>
        /// Отчество директора
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Тип организации
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// ИНН организации
        /// </summary>
        public long Inn { get; set; }

        /// <summary>
        /// КПП организации
        /// </summary>
        public long? Kpp { get; set; }

        /// <summary>
        /// Юридический адресс организации
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Номер расчётного счёта
        /// </summary>
        public string? Rs { get; set; }

        /// <summary>
        /// Номер корреспондентского счёта
        /// </summary>
        public string? Ks { get; set; }

        /// <summary>
        /// Банковский идентификационный код (БИК)
        /// </summary>
        public string? Bik { get; set; }

        /// <summary>
        /// Полное наименование банка
        /// </summary>
        public string? Bank { get; set; }

        /// <summary>
        /// Общероссийский классификатор организаций и предприятий
        /// </summary>
        public long? Okpo { get; set; }

        /// <summary>
        /// Основной государственный регистрационный номер
        /// </summary>
        public long? Ogrn { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string? Email { get; set; }

        public ICollection<Contract> Contract { get; set; }
    }
}
