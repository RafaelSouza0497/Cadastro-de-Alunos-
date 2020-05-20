using AppMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroItaka
{
    public partial class TelaLogin : Form
    {
        public TelaLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
           
            string loginPadrao = "itaka";
            string senhaPadrao = "1234";
           
            string login = txtLogin.Text;
            string senha = txtSenha.Text;
           
            
            if (login == loginPadrao && senha == senhaPadrao )
            {
                timer1.Start();
            }
           
            
            else
            {
                MessageBox.Show("VERIFIQUE O LOGIN E A SENHA NOVAMENTE ", "Campo Inválido ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            
            
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {

            progressBar1.Increment(5);
            if (progressBar1.Value == 100)
            {
                MenuInicial c = new MenuInicial();
                timer1.Stop();
                c.ShowDialog();
                
                progressBar1.Increment(-100);
            }
            

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
