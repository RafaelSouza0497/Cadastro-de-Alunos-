using CadastroItaka.DataSource;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CadastroItaka.Modelos.Pessoas
{
    [Table("Professores")]
    public class Professor : Pessoa, IPapel
    {
        [Column("Formacao")]
        public string Formacao { get => formacao; set => formacao = value; }

        private string formacao;

        public Professor(string nome, string cpf, string rg, string email, string sexo, DateTime dataNascimento, DateTime dataCadastro, string telContato, string telCelular, string formacao)
            : base(nome, cpf, rg, email, sexo, dataNascimento, dataCadastro, telContato, telCelular)
        {
            this.formacao = formacao;
        }

        public bool Cadastrar()
        {
            try
            {
                Contexto ItakaConnection = new Contexto();
                ItakaConnection.Professores.Add(this);
                ItakaConnection.SaveChanges();
                ItakaConnection.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Editar()
        {
            try
            {
                Contexto ItakaConnection = new Contexto();
                ItakaConnection.Entry(this).State = EntityState.Modified;
                ItakaConnection.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Excluir()
        {
            try
            {
                Contexto ItakaConnection = new Contexto();
                ItakaConnection.Entry(this).State = EntityState.Deleted;
                ItakaConnection.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
