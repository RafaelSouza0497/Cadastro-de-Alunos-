using CadastroItaka.DataSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CadastroItaka.Modelos.Pessoas
{
    [Table("Alunos")]
    public class Aluno : Pessoa, IPapel
    {
        [ForeignKey("Responsavel")]
        [Column("IdResponsavelAluno")]
        public int IdResponsavel { get => idResponsavel; set => idResponsavel = value; }

        [Column("StatusAluno")]
        public bool Status { get => status; set => status = value; }

        [Column("Aprovacao")]
        public bool Aprovacao { get => aprovacao; set => aprovacao = value; }

        [InverseProperty("Alunos")]
        public Responsavel Responsavel { get => responsavel; set => responsavel = value; }

        private Responsavel responsavel;
        private int idResponsavel;
        private bool status;
        private bool aprovacao;

        public Aluno(string nome, string cpf, string rg, string email, string sexo, DateTime dataNascimento, DateTime dataCadastro, string telContato, string telCelular, bool status, bool aprovacao)
                    : base(nome, cpf, rg, email, sexo, dataNascimento, dataCadastro, telContato, telCelular)
        {
            this.status = status;
            this.aprovacao = aprovacao;

        }

        public bool Cadastrar()
        {
            try
            {
                Contexto ItakaConnection = new Contexto();
                ItakaConnection.Alunos.Add(this);
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
                Aluno aluno = ItakaConnection.Alunos.Find(Id);
                ItakaConnection.Alunos.Remove(aluno);
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

