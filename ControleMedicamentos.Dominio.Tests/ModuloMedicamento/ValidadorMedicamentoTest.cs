using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTest
    {
        public ValidadorMedicamentoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }


        [TestMethod]
        public void nome_medicamento_nao_pode_ser_vazio()
        {
            Medicamento medicamento = new("", "remedio para dor", "20213/2", Convert.ToDateTime("10/05/2032"));

            var validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void descricao_medicamento_nao_pode_ser_vazio()
        {
            Medicamento medicamento = new("Luis","", "20213/2", Convert.ToDateTime("10/05/2032"));

            var validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Descricao' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }



        [TestMethod]
        public void lote_medicamento_nao_pode_ser_vazio()
        {
            Medicamento medicamento = new("Luis", "remedio para dor", "", Convert.ToDateTime("10/05/2032"));

            var validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Lote' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }



        [TestMethod]
        public void validade_medicamento_nao_pode_ser_vazio()
        {
            Medicamento medicamento = new("Luis", "remedio para dor", "2013", Convert.ToDateTime(null));

            var validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Validade' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void quantidade_medicamento_nao_pode_ser_vazio()
        {
            Medicamento medicamento = new("Dorflex", "remedio para dor", "2013", Convert.ToDateTime("10/05/2022"));
            medicamento.QuantidadeDisponivel = 0;

            var validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'QuantidadeDisponivel' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }
    }
}
