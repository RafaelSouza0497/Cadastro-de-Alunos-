using AppAluno;
using AppAluno.Cadastros;
using AppCurso;
using AppProfessor;
using AppTurma;
using System;
using System.Windows.Forms;

namespace AppMenu
{
    public partial class MenuInicial : Form
    {
        public MenuInicial()
        {
            InitializeComponent();
        }
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void cadastrarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            timerCadProf.Start();
        }
        private void cadastrarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timerCadTurmas.Start();
        }
        private void adicionarProfessoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerAddProf.Start();
        }
        private void cadastrarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timerCadCurso.Start();
        }
        private void matricularAlunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerMatriAluno.Start();
        }
        private void frequênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerListaAluno.Start();
        }
        private void responsavelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerCadResp.Start();
        }
        private void alunoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerCadAluno.Start();
        }
        private void listaChamadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerListaDeCham.Start();           
        }
        private void listaResponsávelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerListaResp.Start();
        }
        private void listaDeProfessoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerListaCurso.Start();
        }
        private void turmasCadastradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerListaTurmas.Start();            
        }
        private void listaDeProfessoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timerListaDeProf.Start();
        }
        private void timerCadResp_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {              
                CADResponsavelAluno responsaveis = new CADResponsavelAluno();
                timerCadResp.Stop();
                responsaveis.ShowDialog();
                progressBar1.Increment(-100);
            }
        }
        private void timerCadAluno_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                CADAluno alunos = new CADAluno();
                timerCadAluno.Stop();
                alunos.ShowDialog();
                progressBar1.Increment(-100);
            }            
        }
        private void timerListaAluno_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                CadLista c = new CadLista();
                timerListaAluno.Stop();
                c.ShowDialog();
                progressBar1.Increment(-100);
            }            
        }
        private void timerListaResp_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                ListaResponsavel l = new ListaResponsavel();
                timerListaResp.Stop();
                l.ShowDialog();
                progressBar1.Increment(-100);
            }           
        }
        private void timerCadCurso_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                CadCurso c = new CadCurso();
                timerCadCurso.Stop();
                c.ShowDialog();
                progressBar1.Increment(-100);
            }           
        }
        private void timerAddProf_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                ControleCurso c = new ControleCurso();
                timerAddProf.Stop();
                c.ShowDialog();
                progressBar1.Increment(-100);
            }            
        }
        private void timerListaCurso_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                ListaCurso c = new ListaCurso();
                timerListaCurso.Stop();
                c.ShowDialog();
                progressBar1.Increment(-100);
            }           
        }
        private void timerCadTurmas_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                CadTurma c = new CadTurma();
                timerCadTurmas.Stop();
                c.ShowDialog();
                progressBar1.Increment(-100);
            }           
        }
        private void timerMatriAluno_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                CadAlunoNaTurma c = new CadAlunoNaTurma();
                timerMatriAluno.Stop();
                c.ShowDialog();
                progressBar1.Increment(-100);
            }            
        }
        private void timerListaDeCham_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                Presença p = new Presença();
                timerListaDeCham.Stop();
                p.ShowDialog();
                progressBar1.Increment(-100);
            }          
        }
        private void timerListaTurmas_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                ListaTurma l = new ListaTurma();
                timerListaTurmas.Stop();
                l.ShowDialog();
                progressBar1.Increment(-100);
            }
        }
        private void timerCadProf_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                CadProfessor c = new CadProfessor();
                timerCadProf.Stop();
                c.ShowDialog();
                progressBar1.Increment(-100);
            }           
        }
        private void timerListaDeProf_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == 100)
            {
                ListaProfessor prof = new ListaProfessor();
                timerListaDeProf.Stop();
                prof.ShowDialog();
                progressBar1.Increment(-100);
            }           
        }
    }
}
