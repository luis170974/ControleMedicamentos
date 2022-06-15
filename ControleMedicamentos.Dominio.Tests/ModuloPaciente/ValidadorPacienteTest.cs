using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTest
    {
        public ValidadorPacienteTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }


        [TestMethod]
        public void nome_paciente_nao_pode_ser_vazio()
        {
            Paciente paciente = new("", "82953499415521");

            var validador = new ValidadorPaciente();

            var resultado = validador.Validate(paciente);

            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void cartao_sus_paciente_nao_pode_ser_vazio()
        {
            Paciente paciente = new("Luis", "");

            var validador = new ValidadorPaciente();

            var resultado = validador.Validate(paciente);

            Assert.AreEqual("'CartaoSUS' somente quinze numeros", resultado.Errors[0].ErrorMessage);
        }
    }
}
