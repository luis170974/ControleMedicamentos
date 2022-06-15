using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest
    {
        private Paciente paciente;
        private RepositorioPacienteEmBancoDados repositorio;

        public RepositorioPacienteEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");
            paciente = new Paciente("Leandro", "123456789012345");
            repositorio = new RepositorioPacienteEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_paciente()
        {
            repositorio.Inserir(paciente);

            Paciente pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);


        }

        [TestMethod]
        public void Deve_editar_paciente()
        {
            repositorio.Inserir(paciente);


            paciente.Nome = "William Ludwig De Souza";
            paciente.CartaoSUS = "123456789012345";



            repositorio.Editar(paciente);

            Paciente pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);


        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {
            repositorio.Inserir(paciente);

            repositorio.Excluir(paciente);

            var pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);
            Assert.IsNull(pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_pacientes()
        {
            repositorio.Inserir(paciente);



            var pacientes = repositorio.SelecionarTodos();

            Assert.AreEqual(1, pacientes.Count);
        }


        [TestMethod]
        public void Deve_selecionar_um_pacientes()
        {
            repositorio.Inserir(paciente);

            Paciente pacienteEncontrado = repositorio.SelecionarPorId(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);


        }


    }
}
