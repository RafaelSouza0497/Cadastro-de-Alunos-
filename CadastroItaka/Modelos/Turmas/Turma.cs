using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Pessoas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CadastroItaka.Modelos.Matriculados
{
    [Table("Turmas")]
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdTurma")]
        public int Id { get => id; set => id = value; }

        [Column("Turma")]
        public string IDTurma { get => IdTurma; set => IdTurma = value; }

        //[ForeignKey("Curso")]
        [Column("Curso")]
		public string Atividade { get => atividade; set => atividade = value; }

        //[ForeignKey("Professor")]
        [Column("Professor")]
		public string Docente { get => docente; set => docente = value; }

		[Column("Turno")]
        public string Turno { get => turno; set => turno = value; }

        [Column("DataInicial")]
        public DateTime DataInicial { get => dataInicial; set => dataInicial = value; }

        [Column("DataFinal")]
        public DateTime DataFinal { get => dataFinal; set => dataFinal = value; }

        [Column("HoraInicial")]
        public TimeSpan HoraInicial { get => horaInicial; set => horaInicial = value; }

        [Column("HoraFinal")]
        public TimeSpan HoraFinal { get => horaFinal; set => horaFinal = value; }

        //public virtual Curso Curso { get => curso; set => curso = value; }

        //public virtual Professor Professor { get => professor; set => professor = value; }

        public List<Matriculado> Matriculados { get => matriculados; set => matriculados = value; }

        private int id;
        private string IdTurma;
        private string turno;
        private string atividade;
        private Curso curso;
        private string docente;
        private Professor professor;

        private DateTime dataInicial;
        private DateTime dataFinal;
        private TimeSpan horaInicial;
        private TimeSpan horaFinal;
        private List<Matriculado> matriculados;

        public Turma(string IdTurma, string atividade, string docente, string turno, DateTime dataInicial, DateTime dataFinal, TimeSpan horaInicial, TimeSpan horaFinal)
        {
            this.IdTurma = IdTurma;
            this.turno = turno;
            this.atividade = atividade;
            this.docente = docente;
            this.dataInicial = dataInicial;
            this.dataFinal = dataFinal;
            this.horaInicial = horaInicial;
            this.horaFinal = horaFinal;
        }

        public TimeSpan GetHoraAula()
        {
            TimeSpan HoraAula = horaInicial.Subtract(horaFinal);
            return HoraAula;
        }

        public bool Cadastrar()
		{
			try
			{
				Contexto ItakaConnection = new Contexto();
		
				ItakaConnection.Turmas.Add(this);
				ItakaConnection.SaveChanges();
				ItakaConnection.Dispose();

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

        public bool Editar()
        {
            throw new NotImplementedException();
        }
    }
}
