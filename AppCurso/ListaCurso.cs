using AppTurma;
using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Matriculados;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AppCurso
{
    public partial class ListaCurso : Form
    {
        private Contexto db;

        public ListaCurso()
        {
            InitializeComponent();
            db = new Contexto();
        }

        private void ListaCurso_Load(object sender, EventArgs e)
        {
            GetCursos();
        }

        /// <summary>
        /// Busca cursos cadastrados no sistema.
        /// </summary>
        private void GetCursos()
        {
            var query = from curso in db.Cursos
                        select new
                        {
                            Matricula = curso.Id,
                            Atividade = curso.Atividade,
                            CargaHoraria = curso.CargaHoraria
                        };

            dgvCursos.DataSource = query.ToList();
        }

        #region Button
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridAlunoRow = dgvCursos.SelectedRows;

            foreach (DataGridViewRow rowA in gridAlunoRow)
            {
                int matricula = int.Parse(dgvCursos.Rows[rowA.Index].Cells["Matricula"].Value.ToString());

                CadCurso cadCurso = new CadCurso();
                cadCurso.GetCurso(matricula);
                cadCurso.ShowDialog();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridAlunoRow = dgvCursos.SelectedRows;

            foreach (DataGridViewRow rowA in gridAlunoRow)
            {
                int matricula = int.Parse(dgvCursos.Rows[rowA.Index].Cells["Matricula"].Value.ToString());

                string atividade = db.Cursos.Where(x => x.Id == matricula).Select(y => y.Atividade).FirstOrDefault();
                int cargaHoraria = db.Cursos.Where(x => x.Id == matricula).Select(y => y.CargaHoraria).FirstOrDefault();

                Curso curso = new Curso(atividade, cargaHoraria);
                curso.Id = matricula;

                if (curso.Excluir())
                {
                    MessageBox.Show("Curso excluído com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBoxButtons.OK == 0)
                    {
                        GetCursos();
                        dgvCursos.Refresh();
                    }
                }
            }
        }

        private void btnTurmas_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridAlunoRow = dgvCursos.SelectedRows;

            foreach (DataGridViewRow rowA in gridAlunoRow)
            {
                string curso = dgvCursos.Rows[rowA.Index].Cells["Atividade"].Value.ToString();

                ListaTurma listaT = new ListaTurma();
                listaT.Operacao = "turmasCursos";
                listaT.GetTurmas(curso);
                listaT.ShowDialog();
            }

        }
        #endregion
    }
}
