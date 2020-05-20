using CadastroItaka.DataSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CadastroItaka.Modelos.Pessoas
{
    [Table("Responsaveis")]
    public class Responsavel : Pessoa, IPapel
    {
        [Column("GrauParentescoResponsavel")]
        public string GrauParentesco { get => grauParentesco; set => grauParentesco = value; }

        [Column("OcupacaoResponsavel")]
        public string Ocupacao { get => ocupacao; set => ocupacao = value; }

        [Column("RendaResponsavel")]
        public double Renda { get => renda; set => renda = value; }

        [Column("NomeContatoResponsavel")]
        public string NomeContato { get => nomeContato; set => nomeContato = value; }

        public List<Aluno> Alunos { get => alunos; set => alunos = value; }

        private string grauParentesco;
        private string ocupacao;
        private double renda;
        private string nomeContato;
        private List<Aluno> alunos;

        public Responsavel(string nome, string cpf, string rg, string email, string sexo, DateTime dataNascimento, DateTime dataCadastro, string telContato, string telCelular,
                           string grauParentesco, string ocupacao, double renda, string nomeContato)
                           : base(nome, cpf, rg, email, sexo, dataNascimento, dataCadastro, telContato, telCelular)
        {
            this.grauParentesco = grauParentesco;
            this.ocupacao = ocupacao;
            this.renda = renda;
            this.nomeContato = nomeContato;
           
        }

        public bool Cadastrar()
        {
            try
            {
                Contexto ItakaConnection = new Contexto();
                ItakaConnection.Responsaveis.Add(this);
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
                Responsavel responsavel = ItakaConnection.Responsaveis.Find(Id);
                ItakaConnection.Responsaveis.Remove(responsavel);
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