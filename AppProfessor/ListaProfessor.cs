using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Pessoas;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AppProfessor
{
    public partial class ListaProfessor : Form
    {
        private Contexto db;

        public ListaProfessor()
        {
            InitializeComponent();
            db = new Contexto();
        }

        private void ListaProfessor_Load(object sender, EventArgs e)
        {
            GetProfessores();
        }

        /// <summary>
        /// Busca professores cadastrados no sistema.
        /// </summary>
        private void GetProfessores()
        {
            var query = from pessoa in db.Pessoas
                        join professor in db.Professores on pessoa.Id equals professor.Id
                        select new
                        {
                            Matricula = pessoa.Id,
                            Nome = pessoa.Nome,
                            Rg = pessoa.Rg,
                            Cpf = pessoa.Cpf,
                            Formacao = professor.Formacao,
                            Email = pessoa.Email,
                            DataCadastro = pessoa.DataCadastro,
                        };

            dgvProfessores.DataSource = query.ToList();
        }

        #region Button
        private void btnEditar_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridAlunoRow = dgvProfessores.SelectedRows;

            foreach (DataGridViewRow rowA in gridAlunoRow)
            {
                int matricula = int.Parse(dgvProfessores.Rows[rowA.Index].Cells["Matricula"].Value.ToString());

                CadProfessor cadProfessor = new CadProfessor();
                cadProfessor.GetProfessor(matricula);
                cadProfessor.ShowDialog();
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridAlunoRow = dgvProfessores.SelectedRows;

            foreach (DataGridViewRow rowA in gridAlunoRow)
            {
                int matricula = int.Parse(dgvProfessores.Rows[rowA.Index].Cells["Matricula"].Value.ToString());

                string nome = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.Nome).FirstOrDefault();
                string cpf = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.Cpf).FirstOrDefault();
                string rg = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.Rg).FirstOrDefault();
                string email = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.Email).FirstOrDefault();
                string sexo = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.Sexo).FirstOrDefault();
                DateTime dataNascimento = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.DataNascimento).FirstOrDefault();
                DateTime dataCadastro = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.DataCadastro).FirstOrDefault();
                string telContato = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.TelContato).FirstOrDefault();
                string telCelular = db.Pessoas.Where(x => x.Id == matricula).Select(y => y.TelCelular).FirstOrDefault();
                var formacaoProf = from pessoa in db.Pessoas
                                   join prof in db.Professores on pessoa.Id equals prof.Id
                                   where pessoa.Id == matricula
                                   select new
                                   {
                                       prof.Formacao
                                   };

                string formacao = formacaoProf.ToString();

                Professor professor = new Professor(nome, cpf, rg, email, sexo, dataNascimento, dataCadastro, telContato, telCelular, formacao);
                professor.Id = matricula;

                if (professor.Excluir())
                {
                    MessageBox.Show("Professor excluído com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBoxButtons.OK == 0)
                    {
                        GetProfessores();
                        dgvProfessores.Refresh();
                    }
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
