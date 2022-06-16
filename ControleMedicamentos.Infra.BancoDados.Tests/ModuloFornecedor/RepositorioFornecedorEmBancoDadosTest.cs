using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        private Fornecedor fornecedor;
        private RepositorioFornecedorEmBancoDados repositorio;


        public RepositorioFornecedorEmBancoDadosTest()
        {
            SqlDelete();
            InstanciandoObjeto();
            InstanciandoRepositorio();
        }

        private void InstanciandoRepositorio()
        {
            repositorio = new RepositorioFornecedorEmBancoDados();
        }

        private void InstanciandoObjeto()
        {
            fornecedor = new Fornecedor("Luis", "49998319930", "luishenriquekraus@hotmail.com", "Otacilio Costa", "SC");
        }

        private static void SqlDelete()
        {
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
        }

        [TestMethod]
        public void Deve_inserir_fornecedor()
        {
            repositorio.Inserir(fornecedor);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);



        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            repositorio.Inserir(fornecedor);


            fornecedor.Nome = "William Ludwig De Souza";
            fornecedor.Telefone = "49998319930";
            fornecedor.Email = "willudwig@hotmail.com";
            fornecedor.Cidade = "Lages";
            fornecedor.Estado = "SC";


            repositorio.Editar(fornecedor);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);



        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {

            repositorio.Inserir(fornecedor);

            repositorio.Excluir(fornecedor);

            var pacienteEncontrado = repositorio.SelecionarPorId(fornecedor.Id);
            Assert.IsNull(pacienteEncontrado);

        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores()
        {
            repositorio.Inserir(fornecedor);



            var fornecedores = repositorio.SelecionarTodos();

            Assert.AreEqual(1, fornecedores.Count);


        }

        [TestMethod]
        public void Deve_selecionar_um_fornecedor()
        {
            repositorio.Inserir(fornecedor);


            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);

        }
    }
}
