using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Controladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AppCurso
{
    public partial class ControleCurso : Form
	{
		private Contexto db;

		public ControleCurso()
		{
			InitializeComponent();
			db = new Contexto();
			lbProf.SelectionMode = SelectionMode.MultiExtended;
		}

		private void ControleCurso_Load(object sender, EventArgs e)
		{
			List<string> LstCurso = new List<string>();
			LstCurso = db.Cursos.Select(y => y.Atividade).ToList();
			foreach (var item in LstCurso)
			{
				cbCursos.Items.Add(item);
			}

			List<string> LstProf = new List<string>();
			LstProf = db.Professores.Select(y => y.Nome).ToList();
			foreach (var item in LstProf)
			{
				lbProf.Items.Add(item);
			}
		}

		private void BtnSalvar_Click(object sender, EventArgs e)
		{

			int IdProf;
			string atividadeCurso = cbCursos.Text;
			int IdCurso = db.Cursos.Where(x => x.Atividade == atividadeCurso).Select(y => y.Id).FirstOrDefault();

			foreach (var item in lbProf.SelectedItems)
			{
				IdProf = db.Professores.Where(x => x.Nome == item.ToString()).Select(y => y.Id).FirstOrDefault();
				ControleCursos control = new ControleCursos(IdCurso, IdProf);
                if (control.Cadastrar())
                {
                    MessageBox.Show("Professor registrado com sucesso!", "Controle de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBoxButtons.OK == 0)
                    {
                        cbCursos.Text = string.Empty;
                    }
                }
			}

		}

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
