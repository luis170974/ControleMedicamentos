using ControleMedicamentos.Dominio.ModuloPaciente;
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
            paciente = new Paciente("Leandro", "99983199302136");
            repositorio = new RepositorioPacienteEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_paciente()
        {

            Paciente pacienteEncontrado = repositorio.SelecionarUnico(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);


        }

        [TestMethod]
        public void Deve_editar_paciente()
        {

            Paciente pacienteAtualizado = repositorio.SelecionarUnico(paciente.Id);

            pacienteAtualizado.Nome = "Luis Kraus";
            pacienteAtualizado.CartaoSUS = "99953199612136";


            repositorio.Editar(pacienteAtualizado);

            Paciente pacienteEncontrado = repositorio.SelecionarUnico(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);


        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {

            repositorio.Excluir(paciente);

            Assert.IsNull(paciente);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_pacientes()
        {

            Paciente pacienteDois = new Paciente("Matheus Medeiros", "82953499415521");

            repositorio.Inserir(pacienteDois);

            Paciente pacienteTres = new Paciente("Ana Beatriz", "94123125656176");

            repositorio.Inserir(pacienteTres);

            var pacientes = repositorio.SelecionarTodos();

            Assert.AreEqual(3, pacientes.Count);

            Assert.AreEqual("Leandro", pacientes[0].Nome);
            Assert.AreEqual("Matheus Medeiros", pacientes[1].Nome);
            Assert.AreEqual("Ana Beatriz", pacientes[2].Nome);
        }


        [TestMethod]
        public void Deve_selecionar_um_pacientes()
        {

            Paciente pacienteEncontrado = repositorio.SelecionarUnico(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);


        }


    }
}
