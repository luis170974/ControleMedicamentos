using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
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
        private Fornecedor fornecedor;

        private RepositorioMedicamentoEmBancoDados repositorio;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;


        public RepositorioMedicamentoEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            medicamento = new Medicamento("Ibuprofeno", "Remedio Pra dor De cabeça", "2022", Convert.ToDateTime("10/05/2022"));
            fornecedor = new Fornecedor("Luis", "49998319930", "luishenriquekraus@hotmail.com", "Otacilio Costa", "SC");
            repositorio = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {


            repositorioFornecedor.Inserir(medicamento.Fornecedor);

            medicamento.Fornecedor.Id = 1;

            repositorio.Inserir(medicamento);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);




        }

        [TestMethod]
        public void Deve_editar_medicamento()
        {

            Medicamento medicamentoAtualizado = repositorio.SelecionarPorId(medicamento.Id);

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

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

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

            repositorio.Inserir(medicamento);
            var medicamentoDois = new Medicamento("Maleato de Descloferninamina", "Remedio Pra Alergia", "2032", Convert.ToDateTime("10/05/2030"));
            var medicamentoTres = new Medicamento("Paracetamol", "Remedio Pra dor De cabeça", "2027", Convert.ToDateTime("10/05/2031"));

            #endregion


            repositorio.Inserir(medicamentoDois);

            
            repositorio.Inserir(medicamentoTres);

            var medicamentos = repositorio.SelecionarTodos();

            Assert.AreEqual(3, medicamentos.Count);

            Assert.AreEqual(medicamento.Nome, medicamentos[0].Nome);
            Assert.AreEqual(medicamentoDois.Nome, medicamentos[1].Nome);
            Assert.AreEqual(medicamentoTres.Nome, medicamentos[2].Nome);
        }

        [TestMethod]
        public void Deve_selecionar_um_medicamento()
        {


            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);

        }


    }
}
