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
            SqlDelete();

            InstanciaObjetos();

            MedicamentoRecebeOsObjetos();

            InstanciaRepositorio();
        }

        private void InstanciaRepositorio()
        {
            repositorio = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
        }

        private void MedicamentoRecebeOsObjetos()
        {
            medicamento.Fornecedor = fornecedor;

            medicamento.Fornecedor.Id = 1;

            medicamento.Id = 1;

            medicamento.QuantidadeDisponivel = 10;
        }

        private void InstanciaObjetos()
        {
            fornecedor = new Fornecedor("Luis", "49998319930", "luishenriquekraus@hotmail.com", "Otacilio Costa", "SC");

            medicamento = new Medicamento("Ibuprofeno", "Remedio Pra dor De cabeça", "2022", Convert.ToDateTime("10/05/2022"));
        }

        private static void SqlDelete()
        {
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {


            repositorioFornecedor.Inserir(medicamento.Fornecedor);

            

            repositorio.Inserir(medicamento);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(medicamento.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(medicamento.QuantidadeDisponivel, medicamentoEncontrado.QuantidadeDisponivel);




        }

        [TestMethod]
        public void Deve_editar_medicamento()
        {

            repositorioFornecedor.Inserir(medicamento.Fornecedor);



            repositorio.Inserir(medicamento);



            medicamento.Nome = "William Ludwig De Souza";
            medicamento.Descricao = "willudwig10";
            medicamento.Lote = "12346";
            medicamento.Validade = Convert.ToDateTime("10/02/2025");
            medicamento.Fornecedor.Nome = "Luis Kraus";
            medicamento.Fornecedor.Telefone = "49998319910";
            medicamento.Fornecedor.Email = "luishenriquekraus@gmail.com";
            medicamento.Fornecedor.Cidade = "Lages";
            medicamento.Fornecedor.Estado = "SC";



            repositorio.Editar(medicamento);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(medicamento.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(medicamento.QuantidadeDisponivel, medicamentoEncontrado.QuantidadeDisponivel);


        }

        [TestMethod]
        public void Deve_excluir_medicamento()
        {
            repositorioFornecedor.Inserir(medicamento.Fornecedor);


            repositorio.Inserir(medicamento);

            var resultado = repositorio.Excluir(medicamento);


            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos()
        {


            repositorioFornecedor.Inserir(medicamento.Fornecedor);



            repositorio.Inserir(medicamento);


            var medicamentos = repositorio.SelecionarTodos();

            Assert.AreEqual(1, medicamentos.Count);

        }

        [TestMethod]
        public void Deve_selecionar_um_medicamento()
        {

            repositorioFornecedor.Inserir(medicamento.Fornecedor);



            repositorio.Inserir(medicamento);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(medicamento.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(medicamento.QuantidadeDisponivel, medicamentoEncontrado.QuantidadeDisponivel);

        }


    }
}
