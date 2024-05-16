using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Api.Models.Exceptions;

namespace RentalOfPremises.Api.Attribute
{
    /// <summary>
    /// Фильтр, который определяет тип значения и код состояния 406, возвращаемый действием <see cref="ApiExceptionDetail"/>
    /// </summary>
    public class ApiNotAcceptableAttribute : ProducesResponseTypeAttribute
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiNotAcceptableAttribute"/>
        /// </summary>
        public ApiNotAcceptableAttribute() : this(typeof(ApiExceptionDetail))
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiNotAcceptableAttribute"/>
        /// </summary>
        public ApiNotAcceptableAttribute(Type type)
            : base(type, StatusCodes.Status406NotAcceptable)
        {
        }
    }
}
