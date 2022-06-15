﻿using ControleMedicamentos.Dominio;
using FluentValidation.Results;
using System.Collections.Generic;


namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        ValidationResult Inserir(T entidade);
        ValidationResult Editar(T entidade);
        ValidationResult Excluir(T entidade);
        List<T> SelecionarTodos();
        T SelecionarUnico(int numero);
    }
}
