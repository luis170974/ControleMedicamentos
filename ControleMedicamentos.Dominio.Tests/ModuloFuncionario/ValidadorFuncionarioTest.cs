using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario

{
    [TestClass]
    public class ValidadorFuncionarioTest
    {
        public ValidadorFuncionarioTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void nome_funcionario_nao_pode_ser_nulo()
        {
            Funcionario funcionario = new(null, "luiskraus", "123");

            var validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Nome' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void nome_funcionario_nao_pode_ser_vazio()
        {
            Funcionario funcionario = new("", "luiskraus", "123");

            var validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void login_funcionario_nao_pode_ser_nulo()
        {
            Funcionario funcionario = new("Luis", null, "123");

            var validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Login' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void login_funcionario_nao_pode_ser_vazio()
        {
            Funcionario funcionario = new("Luis", "", "123");

            var validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Login' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void senha_funcionario_nao_pode_ser_nulo()
        {
            Funcionario funcionario = new("Luis", "luiskraus", null);

            var validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Senha' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        public void senha_funcionario_nao_pode_ser_vazio()
        {
            Funcionario funcionario = new("Luis", "luiskraus", "");

            var validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Senha' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }




    }
}
