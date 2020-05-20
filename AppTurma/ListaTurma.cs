using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Matriculados;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AppTurma
{
    public partial class ListaTurma : Form
    {
        private Contexto db;
        //variável para auxiliar na operação da tela (carregar turmas de um curso/ carregar todas as turmas)
        public string Operacao { private get => operacao; set => operacao = value; }
        private string operacao = string.Empty;

        public ListaTurma()
        {
            InitializeComponent();
            db = new Contexto();
        }

        private void ListaTurma_Load(object sender, EventArgs e)
        {
            if (!operacao.Equals("turmasCursos"))
            {
                GetTurmas();
            }
        }

        /// <summary>
        /// Busca turmas cadastrados no sistema.
        /// </summary>
        private void GetTurmas()
        {
            var query = from turma in db.Turmas
                        select new
                        {
                            Matricula = turma.Id,
                            Turma = turma.IDTurma,
                            Atividade = turma.Atividade,
                            Professor = turma.Docente,
                            Turno = turma.Turno,
                            DataInicial = turma.DataInicial,
                            DataFinal = turma.DataFinal,
                            HoraInicial = turma.HoraInicial,
                            HoraFinal = turma.HoraFinal
                        };

            dgvTurmas.DataSource = query.ToList();
        }

        /// <summary>
        /// Busca turmas de um determinado curso
        /// </summary>
        /// <param name="curso">Nome do curso (atividade).</param>
        public void GetTurmas(string curso)
        {
            var query = from turma in db.Turmas
                        where turma.Atividade == curso
                        select new
                        {
                            Matricula = turma.Id,
                            Turma = turma.IDTurma,
                            Atividade = turma.Atividade,
                            Professor = turma.Docente,
                            Turno = turma.Turno,
                            DataInicial = turma.DataInicial,
                            DataFinal = turma.DataFinal,
                            HoraInicial = turma.HoraInicial,
                            HoraFinal = turma.HoraFinal
                        };

            dgvTurmas.DataSource = query.ToList();
        }

        #region Button
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridAlunoRow = dgvTurmas.SelectedRows;

            foreach (DataGridViewRow rowA in gridAlunoRow)
            {
                int matricula = int.Parse(dgvTurmas.Rows[rowA.Index].Cells["Matricula"].Value.ToString());

                CadTurma cadTurma = new CadTurma();
                cadTurma.GetTurma(matricula);
                cadTurma.ShowDialog();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridAlunoRow = dgvTurmas.SelectedRows;

            foreach (DataGridViewRow rowA in gridAlunoRow)
            {
                int matricula = int.Parse(dgvTurmas.Rows[rowA.Index].Cells["Matricula"].Value.ToString());

                string turmaId = db.Turmas.Where(x => x.Id == matricula).Select(y => y.IDTurma).FirstOrDefault();
                string atividade = db.Turmas.Where(x => x.Id == matricula).Select(y => y.Atividade).FirstOrDefault();
                string turno = db.Turmas.Where(x => x.Id == matricula).Select(y => y.Turno).FirstOrDefault();
                string curso = db.Turmas.Where(x => x.Id == matricula).Select(y => y.Atividade).FirstOrDefault();
                string professor = db.Turmas.Where(x => x.Id == matricula).Select(y => y.Docente).FirstOrDefault();
                DateTime dataInicial = db.Turmas.Where(x => x.Id == matricula).Select(y => y.DataInicial).FirstOrDefault();
                DateTime dataFinal = db.Turmas.Where(x => x.Id == matricula).Select(y => y.DataFinal).FirstOrDefault();
                TimeSpan horaInicial = db.Turmas.Where(x => x.Id == matricula).Select(y => y.HoraInicial).FirstOrDefault();
                TimeSpan horaFinal = db.Turmas.Where(x => x.Id == matricula).Select(y => y.HoraFinal).FirstOrDefault();

                Turma turma = new Turma(turmaId, atividade, professor, turno, dataInicial, dataFinal, horaInicial, horaFinal);
                turma.Id = matricula;

                if (turma.Excluir())
                {
                    MessageBox.Show("Turma excluída com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBoxButtons.OK == 0)
                    {
                        GetTurmas();
                        dgvTurmas.Refresh();
                    }
                }
            }
        }
        #endregion 
    }
}