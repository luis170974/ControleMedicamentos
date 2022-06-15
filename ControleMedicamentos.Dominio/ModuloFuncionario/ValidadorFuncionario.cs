using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)

                .NotEmpty()
                .WithMessage("'Nome' não pode ser vazio");

            RuleFor(x => x.Login)

                .NotEmpty()
                .WithMessage("'Login' não pode ser vazio");

            RuleFor(x => x.Senha)

                .NotEmpty()
                .WithMessage("'Senha' não pode ser vazio");


        }
    }
}
