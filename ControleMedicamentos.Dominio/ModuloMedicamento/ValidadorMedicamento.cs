using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("'Nome' não pode ser vazio");

            RuleFor(x => x.Descricao)

                .NotEmpty()
                .WithMessage("'Descricao' não pode ser vazio");

            RuleFor(x => x.Lote)

                .NotEmpty()
                .WithMessage("'Lote' não pode ser vazio");

            RuleFor(x => x.Validade)

                .NotEmpty()
                .WithMessage("'Validade' não pode ser vazio");

            RuleFor(x => x.QuantidadeDisponivel)

                .NotEmpty()
                .WithMessage("'QuantidadeDisponivel' não pode ser vazio");

            RuleFor(x => x.Fornecedor)
                .NotEmpty()
                .WithMessage("'Fornecedor' não pode ser vazio");



        }

            
    }
}
