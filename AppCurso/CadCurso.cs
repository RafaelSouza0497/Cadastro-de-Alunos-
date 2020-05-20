using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Matriculados;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AppCurso
{
    public partial class CadCurso : Form
    {
        private Contexto db;
        public CadCurso()
        {
            InitializeComponent();
            db = new Contexto();
        }

        /// <summary>
        /// Limpa campos de cadastro de curso.
        /// </summary>
        private void LimparCampos()
        {
            txtCargaHoraria.Text = string.Empty;
            txtNomeDaAtividade.Text = string.Empty;
        }

        /// <summary>
        /// Método que busca os dados de curso cadastrado e preenche campos do forms 'CadCurso'.
        /// </summary>
        /// <param name="matricula">Número de matrícula do curso (Id).</param>
        public void GetCurso(int matricula)
        {
            var query = from curso in db.Cursos
                        where curso.Id == matricula
                        select new
                        {
                            Atividade = curso.Atividade,
                            CargaHoraria = curso.CargaHoraria
                        };

            txtCargaHoraria.Text = (query.Select(x => x.CargaHoraria).FirstOrDefault()).ToString();
            txtNomeDaAtividade.Text = query.Select(x => x.Atividade).FirstOrDefault();

            txtNomeDaAtividade.Enabled = false;
        }

        #region Button
        private void btnSalvarCurso_Click(object sender, EventArgs e)
        {
            try
            {
                Curso curso = new Curso(txtNomeDaAtividade.Text, int.Parse(txtCargaHoraria.Text));

                int idCurso = db.Cursos.Where(x => x.Atividade == txtNomeDaAtividade.Text).Select(y => y.Id).FirstOrDefault();

                if (!db.Cursos.Where(x => x.Id == idCurso).Any() && curso.Cadastrar())
                {
                    MessageBox.Show("Curso cadastrado com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    curso.Id = idCurso;
                    if (curso.Editar())
                    {
                        MessageBox.Show("Cadastro de Curso editado com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (MessageBoxButtons.OK == 0)
                {
                    LimparCampos();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void btnVoltarCurso_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region KeyPress
        private void txtNomeDaAtividade_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Aceita apenas Letras, BackSpace(apagar) e Espaço
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                MessageBox.Show("Este Campo aceita apenas Letras!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtCargaHoraria_KeyPress(object sender, KeyPressEventArgs e)
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
