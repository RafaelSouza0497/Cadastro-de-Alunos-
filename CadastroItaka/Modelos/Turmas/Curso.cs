using CadastroItaka.DataSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroItaka.Modelos.Matriculados
{
    [Table("Cursos")]
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get => id; set => id = value; }

        [Column("CargaHoraria")]
        public int CargaHoraria { get => cargaHoraria; set => cargaHoraria = value; }

        [Column("Atividade")]
        public string Atividade { get => atividade; set => atividade = value; }

        private int id;
        private string atividade;
        private int cargaHoraria;

        public Curso(string atividade, int cargaHoraria)
        {
            this.atividade = atividade;
            this.cargaHoraria = cargaHoraria;
		
        }

		public bool Cadastrar()
		{
			try
			{
				Contexto ItakaConnection = new Contexto();
				ItakaConnection.Cursos.Add(this);
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
