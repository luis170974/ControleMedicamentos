using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("'Nome' não pode ser vazio");


            RuleFor(x => x.CartaoSUS)
                .MinimumLength(15)
                .WithMessage("'CartaoSUS' somente quinze numeros")
                .MaximumLength(15)
                .WithMessage("'CartaoSUS' somente quinze numeros");
        }
    }
}
