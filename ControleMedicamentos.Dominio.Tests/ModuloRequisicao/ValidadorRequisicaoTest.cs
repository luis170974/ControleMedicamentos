using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class ValidadorRequisicaoTest
    {

        [TestMethod]
        public void medicamento_requisicao_nao_pode_ser_vazia()
        {
            Requisicao requisicao = new();
            requisicao.Medicamento = null;

            var validador = new ValidadorRequisicao();

            var resultado = validador.Validate(requisicao);

            Assert.AreEqual("'Medicamento' não pode ser vazio", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void quantidade_medicamento_requisicao_nao_pode_ser_vazia()
        {
            Requisicao requisicao = new();

            requisicao.Medicamento = new Medicamento("Ibuprofeno","Dor de cabeça","2031",Convert.ToDateTime("23/05/2032"));
            requisicao.QtdMedicamento = 0;

            var validador = new ValidadorRequisicao();

            var resultado = validador.Validate(requisicao);

            Assert.AreEqual("'QtdMedicamento' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }



        [TestMethod]
        public void paciente_requisicao_nao_pode_ser_vazio()
        {
            Requisicao requisicao = new();
            requisicao.Medicamento = new Medicamento("Ibuprofeno", "Dor de cabeça", "2031", Convert.ToDateTime("23/05/2032"));
            requisicao.Paciente = null;
            requisicao.Funcionario = new Funcionario("Bartolomeu","barto02","bartolomeu0202");
            requisicao.QtdMedicamento = 10;

            var validador = new ValidadorRequisicao();

            var resultado = validador.Validate(requisicao);

            Assert.AreEqual("'Paciente' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void funcionario_requisicao_nao_pode_ser_vazio()
        {
            Requisicao requisicao = new();
            requisicao.Medicamento = new Medicamento("Ibuprofeno", "Dor de cabeça", "2031", Convert.ToDateTime("23/05/2032"));
            requisicao.Funcionario = null;
            requisicao.Paciente = new Paciente("Joao do grau", "82923419425321");
            requisicao.QtdMedicamento = 10;


            var validador = new ValidadorRequisicao();

            var resultado = validador.Validate(requisicao);

            Assert.AreEqual("'Funcionario' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }






    }
}
