﻿using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Matriculados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AppTurma
{
    public partial class CadTurma : Form
    {
        private Contexto db;

        public CadTurma()
        {
            InitializeComponent();
            db = new Contexto();
        }

        private void CadTurma_Load(object sender, EventArgs e)
        {
            List<string> LstCursos = new List<string>();

            LstCursos = db.Cursos.Select(y => y.Atividade).ToList();

            foreach (var item in LstCursos)
            {
                cbxCursoTurma.Items.Add(item);
            }

            GetAlunos();
        }

        /// <summary>
        /// Limpa campos de cadastro de turma.
        /// </summary>
        private void LimparCampos()
        {
            txtDataInicio.Text = string.Empty;
            txtDataFinal.Text = string.Empty;
            txtHoraInicio.Text = string.Empty;
            txtHoraFinal.Text = string.Empty;
            txtTurma.Text = string.Empty;
            cbxTurnoTurma.Text = string.Empty;
            cbxCursoTurma.Text = string.Empty;
            CbProf.Text = string.Empty;
        }

        private void cbxCursoTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CbProf.Text = string.Empty;
            CbProf.Items.Clear();

            string Curso = cbxCursoTurma.Text;
            int IdCurso = db.Cursos.Where(x => x.Atividade == Curso).Select(y => y.Id).FirstOrDefault();

            List<int> LstIdProf = new List<int>();
            LstIdProf = db.ControleCurso.Where(x => x.IdCurso == IdCurso).Select(y => y.IdProf).ToList();

            foreach (var item in LstIdProf)
            {
                string NomeProf = db.Professores.Where(x => x.Id == item).Select(y => y.Nome).FirstOrDefault();
                CbProf.Items.Add(NomeProf);
            }
        }

        #region Getters
        /// <summary>
        /// Metódo que busca os alunos matriculados na turma.
        /// </summary>
        /// <param name="matricula">Número de matrícula (Id) da turma.</param>
        private void GetMatriculados(int matricula)
        {
            var matriculados = from matriculado in db.Matriculados
                               where matriculado.IdTurma == matricula
                               select new
                               {
                                   matriculado.Aluno.Nome,
                                   matriculado.Aluno.Rg,
                                   matriculado.DataMatricula
                               };

            foreach (var matriculado in matriculados.ToList())
            {
                dgvMatriculados.Rows.Add(matriculado.Nome, matriculado.Rg, matriculado.DataMatricula.ToShortDateString());
            }

            dgvMatriculados.Refresh();
        }

        /// <summary>
        /// Metódo que busca os alunos cadastrados no sistema (ativos e aprovados).
        /// </summary>
        private void GetAlunos()
        {
            var alunos = from pessoa in db.Pessoas
                         join aluno in db.Alunos on pessoa.Id equals aluno.Id
                         join responsavel in db.Responsaveis on aluno.IdResponsavel equals responsavel.Id
                         where aluno.Status && aluno.Aprovacao
                         select new
                         {
                             Nome = pessoa.Nome,
                             Rg = pessoa.Rg,
                             DataCadastro = pessoa.DataCadastro
                         };

            dgvAlunos.DataSource = alunos.ToList();
        }

        /// <summary>
        /// Método que busca alunos do gridView e faz chamada de cadastro
        /// </summary>
        /// <param name="idResponsavel">Número de matrícula (Id) do Responsável pelo aluno.</param>
        /// <returns></returns>
        private List<Matriculado> GetAlunos(int idTurma)
        {
            List<Matriculado> matriculados = new List<Matriculado>();

            try
            {
                foreach (DataGridViewRow row in dgvMatriculados.Rows)
                {
                    if (row.Index != (dgvMatriculados.Rows.Count - 1))
                    {
                        string nome = dgvAlunos.Rows[row.Index].Cells["Nome"].Value.ToString();

                        string rg = dgvAlunos.Rows[row.Index].Cells["RG"].Value.ToString();
                        int idAluno = db.Alunos.Where(x => x.Rg == rg).Select(y => y.Id).FirstOrDefault();

                        Matriculado matriculado = new Matriculado(idAluno, DateTime.Now.Date);
                        matriculado.IdTurma = idTurma;

                        if (!db.Matriculados.Where(x => x.IdAluno == matriculado.IdAluno && x.IdTurma == idTurma).Any())
                        {
                            matriculado.Cadastrar();
                            matriculados.Add(matriculado);
                        }
                    }
                }

                if (matriculados.Count == 1)
                {
                    MessageBox.Show("Aluno(a) matriculado com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (matriculados.Count > 1)
                {
                    MessageBox.Show("Aluno(a)s matriculado com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return matriculados;
        }

        /// <summary>
        /// Método que busca os dados de turma cadastrada e preenche campos do forms 'CadTurma'.
        /// </summary>
        /// <param name="matricula">Número de matrícula (Id) da turma.</param>
        public void GetTurma(int matricula)
        {
            var query = from turma in db.Turmas
                        where turma.Id == matricula
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

            txtDataInicio.Text = (query.Select(x => x.DataInicial).FirstOrDefault()).ToString();
            txtDataFinal.Text = (query.Select(x => x.DataFinal).FirstOrDefault()).ToString();
            txtHoraInicio.Text = (query.Select(x => x.HoraInicial).FirstOrDefault()).ToString();
            txtHoraFinal.Text = (query.Select(x => x.HoraFinal).FirstOrDefault()).ToString();
            txtTurma.Text = query.Select(x => x.Turma).FirstOrDefault();
            cbxTurnoTurma.Text = query.Select(x => x.Turno).FirstOrDefault();
            cbxCursoTurma.Text = query.Select(x => x.Atividade).FirstOrDefault();
            CbProf.Text = query.Select(x => x.Professor).FirstOrDefault();

            txtTurma.Enabled = false;

            GetMatriculados(matricula);
        }
        #endregion

        #region Button
        private void btnSalvarTurma_Click(object sender, EventArgs e)
        {
            DateTime dataInicio = Convert.ToDateTime(txtDataInicio.Text).Date;
            DateTime dataFim = Convert.ToDateTime(txtDataFinal.Text).Date;
            TimeSpan horaInicio = TimeSpan.Parse(txtHoraInicio.Text);
            TimeSpan horaFinal = TimeSpan.Parse(txtHoraFinal.Text);
            string turno = cbxTurnoTurma.Text;
            string curso = cbxCursoTurma.Text;
            string professor = CbProf.Text;
            string turmaId = txtTurma.Text;

            Turma turma = new Turma(turmaId, curso, professor, turno, dataInicio, dataFim, horaInicio, horaFinal);

            if (!db.Turmas.Where(x => x.IDTurma == turmaId).Any() && turma.Cadastrar())
            {
                turma.Matriculados = GetAlunos(turma.Id);

                MessageBox.Show("Turma cadastrada com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (MessageBoxButtons.OK == 0)
                {
                    LimparCampos();
                    dgvMatriculados.Rows.Clear();
                }
            }
            else
            {
                turma.Id = db.Turmas.Where(x => x.IDTurma == turmaId).Select(y => y.Id).FirstOrDefault();
                if (turma.Editar())
                {
                    turma.Matriculados = GetAlunos(turma.Id);
                }
            }
        }

        private void btnMatricular_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection gridRow = dgvAlunos.SelectedRows;

            foreach (DataGridViewRow row in gridRow)
            {
                string rg = dgvAlunos.Rows[row.Index].Cells["RG"].Value.ToString();
                string nome = dgvAlunos.Rows[row.Index].Cells["Nome"].Value.ToString();
                string dataCadastro = dgvAlunos.Rows[row.Index].Cells["DataCadastro"].Value.ToString();

                dgvMatriculados.Rows.Add(nome, rg, DateTime.Now.Date.ToShortDateString());
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Deseja remover o aluno desta turma?", "Controle de Cadastro", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridViewSelectedRowCollection gridAlunoRow = dgvMatriculados.SelectedRows;

                foreach (DataGridViewRow rowA in gridAlunoRow)
                {
                    string rg = dgvAlunos.Rows[rowA.Index].Cells["RG"].Value.ToString();
                    int idAluno = db.Alunos.Where(x => x.Rg == rg).Select(y => y.Id).FirstOrDefault();
                    DateTime dataMatricula = db.Matriculados.Where(x => x.Aluno.Rg == rg).Select(y => y.DataMatricula).FirstOrDefault();

                    Matriculado matriculado = new Matriculado(idAluno, dataMatricula);
                    matriculado.IdTurma = db.Turmas.Where(x => x.IDTurma == txtTurma.Text).Select(y => y.Id).FirstOrDefault();

                    if (matriculado.Excluir())
                    {
                        MessageBox.Show("Aluno removido da turma com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            dgvMatriculados.Rows.RemoveAt(dgvMatriculados.CurrentRow.Index);

        }

        private void btnVoltarTurma_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSairTurma_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region KeyPress
        private void txtDataInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Aceita apenas Numeros, BackSpace(apagar) e Espaço
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo aceita apenas Letras!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtDataFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Aceita apenas Numeros, BackSpace(apagar) e Espaço
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo aceita apenas Letras!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtHoraInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Aceita apenas Numeros, BackSpace(apagar) e Espaço
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo aceita apenas Letras!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtHoraFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Aceita apenas Numeros, BackSpace(apagar) e Espaço
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo aceita apenas Letras!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion
    }
}