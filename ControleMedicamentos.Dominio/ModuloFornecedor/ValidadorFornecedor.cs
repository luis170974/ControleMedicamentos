using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)

                .NotEmpty()
                .WithMessage("'Nome' não pode ser vazio");


            RuleFor(x => x.Telefone)
                .MinimumLength(11)
                .WithMessage("'Telefone' somente onze numeros")
                .MaximumLength(11)
                .WithMessage("'Telefone' somente onze numeros");


            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("'Email' formato incorreto");

            RuleFor(x => x.Estado)
                .MinimumLength(2)
                .WithMessage("'Estado' somente duas letras")
                .MaximumLength(2)
                .WithMessage("'Estado' somente duas letras");



        }
    }
}
