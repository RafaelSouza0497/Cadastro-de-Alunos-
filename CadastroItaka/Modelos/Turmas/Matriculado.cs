using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Pessoas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CadastroItaka.Modelos.Matriculados
{
    [Table("Matriculados")]
    public class Matriculado : IPapel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Matricula")]
        public int Matricula { get => matricula; set => matricula = value; }

        [ForeignKey("Aluno")]
        [Column("IdAlunoMatriculado")]
        public int IdAluno { get => idAluno; set => idAluno = value; }

        [ForeignKey("Turma")]
        [Column("IdTurmaMatriculado")]
        public int IdTurma { get => idTurma; set => idTurma = value; }

        [Column("DataMatricula")]
        public DateTime DataMatricula { get => dataMatricula; set => dataMatricula = value; }

        [Column("Falta")]
        public int Falta { get => falta; set => falta = value; }

        [Column("Frequencia")]
        public double Frequencia { get => frequencia; set => frequencia = value; }

        public Aluno Aluno { get => aluno; set => aluno = value; }

        public Turma Turma { get => turma; set => turma = value; }

        public List<bool> Ausencia { get => ausencia; set => ausencia = value; }

        private int matricula;
        private int falta;
        private double frequencia;

        private DateTime dataMatricula;
        private int idAluno;
        private int idTurma;
        private Aluno aluno;

        private Turma turma;
        private List<bool> ausencia = new List<bool>();

        public Matriculado(int idAluno, DateTime dataMatricula)
        {
            this.dataMatricula = dataMatricula;
            this.idAluno = idAluno;
            this.frequencia = 100;
        }

        /// <summary>
        /// Metódo que registra a ausência do aluno
        /// Considerado que o mesmo possui 100% de presença no início do curso
        /// </summary>
        /// <returns></returns>
        public void RegistrarAusencia()
        {
            ausencia.Add(true);
        }

        public double RetornaAusencia()
        {
            int falta = this.Falta++;

            return falta;
        }

        public bool Cadastrar()
        {
            try
            {
                Contexto ItakaConnection = new Contexto();
                ItakaConnection.Matriculados.Add(this);
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
                Matricula = ItakaConnection.Matriculados.Where(x => x.IdAluno == IdAluno && x.IdTurma == IdTurma).Select(y => y.Matricula).FirstOrDefault();
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
