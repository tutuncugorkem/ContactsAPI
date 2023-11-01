using Contacts.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Service.Validations
{
    public class BaseDtoValidator:AbstractValidator<BaseDto>
    {
        public BaseDtoValidator()
        {
            //value type için aralık belirtmen lazım(int double vs), default sıfır olmaması için

            RuleFor(x => x.FullName).NotNull().WithMessage("{PropertyName} is Required").NotEmpty().WithMessage("{PropertyName} is Required");   //property name fluentvalidation libraryden geliyor dinamik isim
            RuleFor(x => x.Email).EmailAddress().NotNull().WithMessage("{PropertyName} is Required").NotEmpty().WithMessage("{PropertyName} is Required");

        }
    }
}
