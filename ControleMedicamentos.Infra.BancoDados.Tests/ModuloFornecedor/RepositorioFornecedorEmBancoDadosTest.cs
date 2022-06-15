using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
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
            fornecedor = new Fornecedor("Luis","49998319930","luishenriquekraus@hotmail.com","Otacilio Costa","Santa Catarina");
            repositorio = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_fornecedor()
        {
            repositorio.Inserir(fornecedor);

            var fornecedorEncontrado = repositorio.SelecionarUnico(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);


           
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            repositorio.Inserir(fornecedor);

            Fornecedor fornecedorAtualizado = repositorio.SelecionarUnico(fornecedor.Id);

            fornecedorAtualizado.Nome = "Luis Kraus";
            fornecedorAtualizado.Telefone = "48998319930";
            fornecedorAtualizado.Cidade = "Sao Paulo";
            fornecedorAtualizado.Estado = "Sao Paulo";

            repositorio.Editar(fornecedorAtualizado);

            var fornecedorEncontrado = repositorio.SelecionarUnico(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);

        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            repositorio.Inserir(fornecedor);

            repositorio.Excluir(fornecedor);

            Assert.IsNull(fornecedor);

        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores()
        {
            repositorio.Inserir(fornecedor);

            Fornecedor fornecedorDois = new Fornecedor("Jonas", "49988317230", "jonas@hotmail.com", "Taio", "Santa Catarina");

            repositorio.Inserir(fornecedorDois);

            Fornecedor fornecedorTres = new Fornecedor("Pedro Xerife", "49983325210", "pedroxerife@hotmail.com", "Lages", "Santa Catarina");

            repositorio.Inserir(fornecedorTres);

            var fornecedores = repositorio.SelecionarTodos();

            Assert.AreEqual(3, fornecedores.Count);

            Assert.AreEqual("Luis", fornecedores[0].Nome);
            Assert.AreEqual("Joao", fornecedores[1].Nome);
            Assert.AreEqual("Pedro", fornecedores[2].Nome);


        }

        [TestMethod]
        public void Deve_selecionar_um_fornecedor()
        {
            repositorio.Inserir(fornecedor);

            var fornecedorEncontrado = repositorio.SelecionarUnico(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);

        }
    }
}
