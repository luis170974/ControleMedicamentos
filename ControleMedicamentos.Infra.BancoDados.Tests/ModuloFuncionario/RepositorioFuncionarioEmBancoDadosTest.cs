using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        private Funcionario funcionario;
        private RepositorioFuncionarioEmBancoDados repositorio;
        public RepositorioFuncionarioEmBancoDadosTest()
        {
            funcionario = new Funcionario("William Ludwig", "willudwig", "1234");
            repositorio = new RepositorioFuncionarioEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_funcionario()
        {

            Funcionario fornecedorEncontrado = repositorio.SelecionarUnico(funcionario.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(funcionario.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(funcionario.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(funcionario.Login, fornecedorEncontrado.Login);
            Assert.AreEqual(funcionario.Senha, fornecedorEncontrado.Senha);

        }

        [TestMethod]
        public void Deve_editar_funcionario()
        {

            Funcionario funcionarioAtualizado = repositorio.SelecionarUnico(funcionario.Id);

            funcionarioAtualizado.Nome = "William Ludwig De Souza";
            funcionarioAtualizado.Login = "willudwig10";
            funcionarioAtualizado.Senha = "12345";


            repositorio.Editar(funcionarioAtualizado);

            Funcionario funcionarioEncontrado = repositorio.SelecionarUnico(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(funcionario.Senha, funcionarioEncontrado.Senha);
            
        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {

            repositorio.Excluir(funcionario);

            Assert.IsNull(funcionario);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_funcionarios()
        {

            Funcionario funcionarioDois = new Funcionario("Alexandre Rech", "alerech07", "alexandrerech01");

            repositorio.Inserir(funcionarioDois);

            Funcionario funcionarioTres = new Funcionario("Tiago Santini", "tsatini01", "dalegremio10");

            repositorio.Inserir(funcionarioTres);

            var funcionarios = repositorio.SelecionarTodos();

            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual("William Ludwig", funcionarios[1].Nome);
            Assert.AreEqual("Alexandre Rech", funcionarios[2].Nome);
            Assert.AreEqual("Tiago Santini", funcionarios[3].Nome);
        }

        [TestMethod]
        public void Deve_selecionar_um_funcionario()
        {

            Funcionario funcionarioEncontrado = repositorio.SelecionarUnico(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(funcionario.Senha, funcionarioEncontrado.Senha);
        }
    }
}
