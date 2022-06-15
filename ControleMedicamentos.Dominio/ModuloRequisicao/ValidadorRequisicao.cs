using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Medicamento)
                .NotEmpty()
                .WithMessage("'Medicamento' não pode ser vazio");

            RuleFor(x => x.QtdMedicamento)
                .NotEmpty()
                .WithMessage("'QtdMedicamento' não pode ser vazio");

            RuleFor(x => x.Data)
                .LessThan(DateTime.Now).WithMessage("'Data' não pode ser menor que a atual")
                .NotEmpty().WithMessage("'Data' não pode ser vazio");


            RuleFor(x => x.Paciente)
                .NotNull().WithMessage("'Paciente' não pode ser nulo");

            RuleFor(x => x.Funcionario)
                .NotNull()
                .WithMessage("'Funcionario' não pode ser nulo");
        }
    }
}
