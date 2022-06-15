using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        private Medicamento medicamento;     

        private RepositorioMedicamentoEmBancoDados repositorio;


        public RepositorioMedicamentoEmBancoDadosTest()
        {
            medicamento = new Medicamento("Ibuprofeno", "Remedio Pra dor De cabeça", "2022", Convert.ToDateTime("10/05/2022"));
            
        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {

            Medicamento medicamentoEncontrado = repositorio.SelecionarUnico(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);




        }

        [TestMethod]
        public void Deve_editar_medicamento()
        {

            Medicamento medicamentoAtualizado = repositorio.SelecionarUnico(medicamento.Id);

            medicamentoAtualizado.Nome = "William Ludwig De Souza";
            medicamentoAtualizado.Descricao = "willudwig10";
            medicamentoAtualizado.Lote = "12346";
            medicamentoAtualizado.Validade = Convert.ToDateTime("10/02/2025");
            medicamentoAtualizado.Fornecedor.Nome = "Luis Kraus";
            medicamentoAtualizado.Fornecedor.Telefone = "49998319910";
            medicamentoAtualizado.Fornecedor.Email = "luishenriquekraus@gmail.com";
            medicamentoAtualizado.Fornecedor.Cidade = "Lages";
            medicamentoAtualizado.Fornecedor.Estado = "Santa Catarina";



            repositorio.Editar(medicamentoAtualizado);

            Medicamento medicamentoEncontrado = repositorio.SelecionarUnico(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);

                   
        }

        [TestMethod]
        public void Deve_excluir_medicamento()
        {

            repositorio.Excluir(medicamento);

            Assert.IsNull(medicamento);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos()
        {
            
           

            #region Medicamentos
            var medicamentoDois = new Medicamento("Maleato de Descloferninamina", "Remedio Pra Alergia", "2032", Convert.ToDateTime("10/05/2030"));
            var medicamentoTres = new Medicamento("Paracetamol", "Remedio Pra dor De cabeça", "2027", Convert.ToDateTime("10/05/2031"));

            #endregion






            repositorio.Inserir(medicamentoDois);

            
            repositorio.Inserir(medicamentoTres);

            var medicamentos = repositorio.SelecionarTodos();

            Assert.AreEqual(3, medicamentos.Count);

            Assert.AreEqual("Ibuprofeno", medicamentos[0].Nome);
            Assert.AreEqual("Maleato de Descloferninamina", medicamentos[1].Nome);
            Assert.AreEqual("Paracetamol", medicamentos[2].Nome);
        }

        [TestMethod]
        public void Deve_selecionar_um_medicamento()
        {


            Medicamento medicamentoEncontrado = repositorio.SelecionarUnico(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);

        }


    }
}
