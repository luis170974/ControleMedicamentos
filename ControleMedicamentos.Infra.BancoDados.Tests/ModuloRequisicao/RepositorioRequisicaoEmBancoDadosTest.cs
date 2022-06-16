using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest
    {
        private Medicamento medicamento;
        private Fornecedor fornecedor;
        private Paciente paciente;
        private Funcionario funcionario;
        private Requisicao requisicao;

        private RepositorioRequisicaoEmBancoDados repositorioRequisicao;
        private RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;
        private RepositorioPacienteEmBancoDados repositorioPaciente;
        private RepositorioFuncionarioEmBancoDados repositorioFuncionario;

        public RepositorioRequisicaoEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");

            requisicao = new Requisicao();

            

            fornecedor = new Fornecedor("Luis", "49998319930", "luishenriquekraus@hotmail.com", "Otacilio Costa", "SC");

            medicamento = new Medicamento("Ibuprofeno", "Remedio Pra dor De cabeça", "2022", Convert.ToDateTime("10/05/2022"));

            paciente = new Paciente("Leandro", "123456789012345");

            funcionario = new Funcionario("William Ludwig", "willudwig", "1234");

            requisicao.Medicamento = medicamento;

            requisicao.Paciente = paciente;

            requisicao.Funcionario = funcionario;

            requisicao.QtdMedicamento = 5;

            requisicao.Id = 1;

            requisicao.Funcionario.Id = 1;

            requisicao.Paciente.Id = 1;

            medicamento.Fornecedor = fornecedor;

            medicamento.Fornecedor.Id = 1;

            medicamento.Id = 1;

            medicamento.QuantidadeDisponivel = 10;

            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_requisicao()
        {

            repositorioMedicamento.Inserir(requisicao.Medicamento);

            repositorioPaciente.Inserir(requisicao.Paciente);

            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);

            repositorioFuncionario.Inserir(requisicao.Funcionario);

            repositorioRequisicao.Inserir(requisicao);

            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);


        }

        [TestMethod]
        public void Deve_editar_requisicao()
        {
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
        }

        [TestMethod]
        public void Deve_selecionar_todas_as_requisicoes()
        {
        }

        [TestMethod]
        public void Deve_selecionar_uma_requisicao()
        {
        }
    }
}
