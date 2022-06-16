using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados : ConexaoBancoDados<Requisicao>, IRepositorio<Requisicao>
    {
        RepositorioFuncionarioEmBancoDados repositorioFuncionario;
        RepositorioPacienteEmBancoDados repositorioPaciente;
        RepositorioMedicamentoEmBancoDados repositorioMedicamento;

        public ValidationResult Inserir(Requisicao entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                InserirRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Editar(Requisicao entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                EditarRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Excluir(Requisicao entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                ExcluirRegistroBancoDados(entidade);

            return resultado;
        }

        public List<Requisicao> SelecionarTodos()
        {
            ConectarBancoDados();

            sql = @"SELECT  
                        R.[ID],
						R.[FUNCIONARIO_ID],
                        R.[PACIENTE_ID],
                        R.[MEDICAMENTO_ID],
                        R.[QUANTIDADEMEDICAMENTO],
                        R.[DATA],
						F.[NOME] AS NOME_FUNCIONARIO,
						M.[NOME] AS NOME_MEDICAMENTO,
						P.[NOME] AS NOME_PACIENTE

                    FROM TBREQUISICAO AS R

                        INNER JOIN TBFUNCIONARIO  AS F ON R.FUNCIONARIO_ID = F.ID
                        INNER JOIN TBPACIENTE    AS P ON R.PACIENTE_ID    = P.ID
                        INNER JOIN TBMEDICAMENTO AS M ON R.MEDICAMENTO_ID = M.ID";

            SqlCommand cmd_Selecao = new(sql, conexao);

            SqlDataReader leitor = cmd_Selecao.ExecuteReader();

            List<Requisicao> requisicoes = LerTodos(leitor);

            DesconectarBancoDados();

            LerFuncionariosPacientesMedicamentos(requisicoes);

            return requisicoes;
        }

        public Requisicao SelecionarPorId(int numero)
        {
            ConectarBancoDados();

            sql = @"SELECT  
                        R.[ID],
                        R.[FUNCIONARIO_ID],
                        R.[PACIENTE_ID],
                        R.[MEDICAMENTO_ID],
                        R.[QUANTIDADEMEDICAMENTO],
                        R.[DATA]

                    FROM TBREQUISICAO AS R

                        INNER JOIN TBFUNCIONARIO  AS F ON R.FUNCIONARIO_ID = F.ID
                        INNER JOIN TBPACIENTE    AS P ON R.PACIENTE_ID = P.ID
                        INNER JOIN TBMEDICAMENTO AS M ON R.MEDICAMENTO_ID = M.ID

                    WHERE R.ID = @ID";

            SqlCommand cmdSelecao = new(sql, conexao);

            cmdSelecao.Parameters.AddWithValue("ID", numero);

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Requisicao reqSelecionada = LerUnico(leitor);

            DesconectarBancoDados();

            Ler_Funcionario_Paciente_Medicamento(reqSelecionada.Funcionario.Id, reqSelecionada.Paciente.Id, reqSelecionada.Medicamento.Id, ref reqSelecionada); // requisicaoSelecionada

            return reqSelecionada;
        }

        #region metodos protected


        protected override void DefinirParametros(Requisicao entidade, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("ID", entidade.Id);
            cmd.Parameters.AddWithValue("FUNCIONARIO_ID", entidade.Funcionario.Id);
            cmd.Parameters.AddWithValue("PACIENTE_ID", entidade.Paciente.Id);
            cmd.Parameters.AddWithValue("MEDICAMENTO_ID", entidade.Medicamento.Id);
            cmd.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", entidade.QtdMedicamento);
            cmd.Parameters.AddWithValue("DATA", entidade.Data);

        }

        protected override void EditarRegistroBancoDados(Requisicao entidade)
        {
            ConectarBancoDados();

            sql = @"UPDATE [TBREQUISICAO] SET 

                        [FUNCIONARIO_ID] = @FUNCIONARIO_ID,
                        [PACIENTE_ID] = @PACIENTE_ID,
                        [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                        [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO,
                        [DATA] = @DATA

                   WHERE
		                 ID = @ID";

            SqlCommand cmd_Edicao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Edicao);

            cmd_Edicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void ExcluirRegistroBancoDados(Requisicao entidade)
        {
            ConectarBancoDados();

            sql = @"DELETE FROM TBREQUISICAO WHERE ID = @ID;";

            SqlCommand cmd_Exclusao = new(sql, conexao);

            cmd_Exclusao.Parameters.AddWithValue("ID", entidade.Id);

            try
            {
                cmd_Exclusao.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                return;
            }

            DesconectarBancoDados();
        }

        protected override void InserirRegistroBancoDados(Requisicao entidade)
        {
            ConectarBancoDados();

            sql = @"INSERT INTO TBREQUISICAO
                           (
                                [FUNCIONARIO_ID],
                                [PACIENTE_ID],
                                [MEDICAMENTO_ID],
                                [QUANTIDADEMEDICAMENTO],
                                [DATA]
                           )
                           VALUES
                           (
                                @FUNCIONARIO_ID,
                                @PACIENTE_ID,
                                @MEDICAMENTO_ID,
                                @QUANTIDADEMEDICAMENTO,
                                @DATA
                           )";

            SqlCommand cmd_Insercao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Insercao);

            cmd_Insercao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override List<Requisicao> LerTodos(SqlDataReader leitor)
        {
            List<Requisicao> requisicoes = new();

            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                int funcionarioId = Convert.ToInt32(leitor["FUNCIONARIO_ID"]);
                int pacienteId = Convert.ToInt32(leitor["PACIENTE_ID"]);
                int medicamentoId = Convert.ToInt32(leitor["MEDICAMENTO_ID"]);
                int quantidadeMedicamento = Convert.ToInt32(leitor["QUANTIDADEMEDICAMENTO"]);
                DateTime data = Convert.ToDateTime(leitor["DATA"]);

                Requisicao requisicao = new Requisicao()
                {
                    Id = id,
                    Data = data,
                    QtdMedicamento = quantidadeMedicamento,

                    Funcionario = new(" ", " ", " ")
                    {
                        Id = funcionarioId,
                    },

                    Paciente = new(" ", " ")
                    {
                        Id = pacienteId,
                    },

                    Medicamento = new(" ", " ", " ", DateTime.Today)
                    {
                        Id = medicamentoId,
                    }
                };

                requisicoes.Add(requisicao);
            }

            return requisicoes;
        }

        protected override Requisicao LerUnico(SqlDataReader leitor)
        {
            Requisicao requisicao = null;

            if (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                int funcionarioId = Convert.ToInt32(leitor["FUNCIONARIO_ID"]);
                int pacienteId = Convert.ToInt32(leitor["PACIENTE_ID"]);
                int medicamentoId = Convert.ToInt32(leitor["MEDICAMENTO_ID"]);
                int quantidadeMedicamento = Convert.ToInt32(leitor["QUANTIDADEMEDICAMENTO"]);
                DateTime data = Convert.ToDateTime(leitor["DATA"]);

                requisicao = new Requisicao()
                {
                    Id = id,
                    Data = data,
                    QtdMedicamento = quantidadeMedicamento,

                    Funcionario = new(" ", " ", " ")
                    {
                        Id = funcionarioId,
                    },

                    Paciente = new(" ", " ")
                    {
                        Id = pacienteId,
                    },

                    Medicamento = new(" ", " ", " ", DateTime.Today)
                    {
                        Id = medicamentoId,
                    }
                };
            }

            return requisicao;
        }

        protected override ValidationResult Validar(Requisicao entidade)
        {
            return new ValidadorRequisicao().Validate(entidade);
        }
        #endregion

        #region metodos privados
        private void LerFuncionariosPacientesMedicamentos(List<Requisicao> requisicoes)
        {
            repositorioFuncionario = new();
            repositorioMedicamento = new();
            repositorioPaciente = new();

            foreach (Requisicao r in requisicoes)
            {
                r.Funcionario = repositorioFuncionario.SelecionarPorId(r.Funcionario.Id);
                r.Paciente = repositorioPaciente.SelecionarPorId(r.Paciente.Id);
                r.Medicamento = repositorioMedicamento.SelecionarPorId(r.Medicamento.Id);
                
            }
        }

        private void Ler_Funcionario_Paciente_Medicamento(int numFuncionario, int numPaciente, int numMedicamento, ref Requisicao req)
        {
            repositorioFuncionario = new();
            repositorioPaciente = new();
            repositorioMedicamento = new();

            req.Funcionario = repositorioFuncionario.SelecionarPorId(numFuncionario);
            req.Paciente = repositorioPaciente.SelecionarPorId(numPaciente);
            req.Medicamento = repositorioMedicamento.SelecionarPorId(numMedicamento);
        }

        #endregion
    }
}
