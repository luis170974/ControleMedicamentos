using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
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
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");
            funcionario = new Funcionario("William Ludwig", "willudwig", "1234");
            repositorio = new RepositorioFuncionarioEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_funcionario()
        {
            repositorio.Inserir(funcionario);

            Funcionario fornecedorEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(funcionario, fornecedorEncontrado);


        }

        [TestMethod]
        public void Deve_editar_funcionario()
        {
            repositorio.Inserir(funcionario);


            funcionario.Nome = "William Ludwig De Souza";
            funcionario.Login = "willudwig10";
            funcionario.Senha = "12345";


            repositorio.Editar(funcionario);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);

        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            repositorio.Inserir(funcionario);

            repositorio.Excluir(funcionario);

            var pacienteEncontrado = repositorio.SelecionarPorId(funcionario.Id);
            Assert.IsNull(pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_funcionarios()
        {
            repositorio.Inserir(funcionario);



            var funcionarios = repositorio.SelecionarTodos();

            Assert.AreEqual(1, funcionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_um_funcionario()
        {
            repositorio.Inserir(funcionario);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);

        }
    }
}
