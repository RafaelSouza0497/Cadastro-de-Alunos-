using CadastroItaka.DataSource;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroItaka.Modelos.Pessoas
{
    [Table("Pessoas")]
    public abstract class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdPessoa")]
        public int Id { get => id; set => id = value; }

        [Column("IdadePessoa")]
        public int Idade { get => idade; set => idade = value; }

        [Column("NomePessoa")]
        public string Nome { get => nome; set => nome = value; }

        [Column("CpfPessoa")]
        public string Cpf
        {
            get => cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            set => cpf = value.Replace(".", string.Empty).Replace("-", string.Empty);
        }

        [Column("RgPessoa")]
        public string Rg { get => rg; set => rg = value; }

        [Column("EmailPessoa")]
        public string Email { get => email; set => email = value; }

        [Column("SexoPessoa")]
        public string Sexo { get => sexo; set => sexo = value; }

        [Column("DataNascimentoPessoa")]
        public DateTime DataNascimento { get => dataNascimento; set => dataNascimento = value; }

        [Column("DataCadastroPessoa")]
        public DateTime DataCadastro { get => dataCadastro; set => dataCadastro = value; }

        [Column("TelCelularPessoa")]
        public string TelCelular
        {
            get => telCelular.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty);
            set => telCelular = value.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty);
        }

        [Column("TelContatoPessoa")]
        public string TelContato
        {
            get => telContato.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty);
            set => telContato = value.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty);
        }

        private int id;
        private int idade;
        private string nome;
        private string cpf;
        private string rg;
        private string email;
        private string sexo;
        private string telContato;
        private string telCelular;
        private DateTime dataNascimento;
        private DateTime dataCadastro;

        public Pessoa(string nome, string cpf, string rg, string email, string sexo, DateTime dataNascimento, DateTime dataCadastro, string telContato, string telCelular)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.rg = rg;
            this.email = email;
            this.sexo = sexo;
            this.dataNascimento = dataNascimento;
            this.dataCadastro = dataCadastro;
            this.idade = DateTime.Now.Year - dataNascimento.Year;
            this.telCelular = telCelular;
            this.telContato = telContato;
        }
    }
}
