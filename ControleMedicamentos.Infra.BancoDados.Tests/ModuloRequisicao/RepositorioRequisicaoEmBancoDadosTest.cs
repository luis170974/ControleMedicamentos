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

            SqlsDeletes();

            InstanciandoObjetos();

            RequisicaoRecebeOsObjetos();

            RequisicaoRecebendosAsIdentificacoes();

            InstanciaDeRepositorios();

        }

        private void InstanciaDeRepositorios()
        {
            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
        }

        private void RequisicaoRecebendosAsIdentificacoes()
        {
            requisicao.Funcionario.Id = 1;
            medicamento.Fornecedor.Id = 1;
            requisicao.Paciente.Id = 1;
            requisicao.Medicamento.Id = 1;
            requisicao.Id = 1;
            requisicao.Medicamento.QuantidadeDisponivel = 10;
        }

        private void RequisicaoRecebeOsObjetos()
        {
            requisicao.Medicamento = medicamento;

            requisicao.Paciente = paciente;

            requisicao.Funcionario = funcionario;

            requisicao.QtdMedicamento = 5;

            medicamento.Fornecedor = fornecedor;
        }

        private void InstanciandoObjetos()
        {
            requisicao = new Requisicao();

            fornecedor = new Fornecedor("Luis", "49998319930", "luishenriquekraus@hotmail.com", "Otacilio Costa", "SC");

            medicamento = new Medicamento("Ibuprofeno", "Remedio Pra dor De cabeça", "2022", Convert.ToDateTime("10/05/2022"));

            paciente = new Paciente("Leandro", "123456789012345");

            funcionario = new Funcionario("William Ludwig", "willudwig", "1234");
        }

        private static void SqlsDeletes()
        {
            Db.ExecutarSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");
        }

        [TestMethod]
        public void Deve_inserir_requisicao()
        {
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);

            repositorioMedicamento.Inserir(requisicao.Medicamento);

            repositorioPaciente.Inserir(requisicao.Paciente);

            repositorioFuncionario.Inserir(requisicao.Funcionario);

            repositorioRequisicao.Inserir(requisicao);

            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao.Id, requisicaoEncontrada.Id);



        }

        [TestMethod]
        public void Deve_editar_requisicao()
        {
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);

            repositorioMedicamento.Inserir(requisicao.Medicamento);

            repositorioPaciente.Inserir(requisicao.Paciente);

            repositorioFuncionario.Inserir(requisicao.Funcionario);

            repositorioRequisicao.Inserir(requisicao);

            requisicao.Medicamento.Nome = "William Ludwig De Souza";
            requisicao.Medicamento.Descricao = "willudwig10";
            requisicao.Medicamento.Lote = "12346";
            requisicao.Medicamento.Validade = Convert.ToDateTime("10/02/2025");
            requisicao.Medicamento.Fornecedor.Nome = "Luis Kraus";
            requisicao.Medicamento.Fornecedor.Telefone = "49998319910";
            requisicao.Medicamento.Fornecedor.Email = "luishenriquekraus@gmail.com";
            requisicao.Medicamento.Fornecedor.Cidade = "Lages";
            requisicao.Medicamento.Fornecedor.Estado = "SC";

            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao.Id, requisicaoEncontrada.Id);
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);

            repositorioMedicamento.Inserir(requisicao.Medicamento);

            repositorioPaciente.Inserir(requisicao.Paciente);

            repositorioFuncionario.Inserir(requisicao.Funcionario);

            repositorioRequisicao.Inserir(requisicao);


            var resultado = repositorioRequisicao.Excluir(requisicao);


            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void Deve_selecionar_todas_as_requisicoes()
        {
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);

            repositorioMedicamento.Inserir(requisicao.Medicamento);

            repositorioPaciente.Inserir(requisicao.Paciente);

            repositorioFuncionario.Inserir(requisicao.Funcionario);

            repositorioRequisicao.Inserir(requisicao);

            var requisicoes = repositorioRequisicao.SelecionarTodos();

            Assert.AreEqual(1, requisicoes.Count);
        }

        [TestMethod]
        public void Deve_selecionar_uma_requisicao()
        {
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);

            repositorioMedicamento.Inserir(requisicao.Medicamento);

            repositorioPaciente.Inserir(requisicao.Paciente);

            repositorioFuncionario.Inserir(requisicao.Funcionario);

            repositorioRequisicao.Inserir(requisicao);

            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao.Id, requisicaoEncontrada.Id);
        }
    }
}
