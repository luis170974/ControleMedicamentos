using ControleMedicamentos.Dominio.ModuloFornecedor;
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
    public class ValidadorFornecedorTest
    {
        public ValidadorFornecedorTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }



        [TestMethod]
        public void nome_fonecedor_nao_pode_ser_vazio()
        {
            Fornecedor fornecedor = new("", "49998319930", "luiskraus@hotmail.com", "Otacilio Costa", "SC");

            var validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }



        [TestMethod]
        public void telefone_fornecedor_nao_pode_ser_vazio()
        {
            Fornecedor fornecedor = new("Luis","", "luiskraus@hotmail.com", "Otacilio Costa", "SC");

            var validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Telefone' somente onze numeros", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void email_fornecedor_nao_pode_ser_incorreto()
        {
            Fornecedor fornecedor = new("Luis", "49998319930", "luiskraushotmail.com", "Otacilio Costa", "SC");

            var validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Email' formato incorreto", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void estado_nao_pode_ter_mais_que_duas_letras()
        {
            Fornecedor fornecedor = new("Luis", "49998319930", "luiskraus@hotmail.com", "Otacilio Costa", "SCE");

            var validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Estado' somente duas letras", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void estado_nao_pode_ter_menos_que_duas_letras()
        {
            Fornecedor fornecedor = new("Luis", "49998319930", "luiskraus@hotmail.com", "Otacilio Costa", "S");

            var validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Estado' somente duas letras", resultado.Errors[0].ErrorMessage);
        }


    }
}

