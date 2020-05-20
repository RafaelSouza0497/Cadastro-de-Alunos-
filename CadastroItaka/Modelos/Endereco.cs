using CadastroItaka.DataSource;
using CadastroItaka.Modelos.Pessoas;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CadastroItaka.Modelos
{
    [Table("Enderecos")]
    public class Endereco
    {	
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get => id; set => id = value; }

        [ForeignKey("Pessoa")]
        [Column("IdPessoaEndereco")]
        public int IdPessoa { get => idPessoa; set => idPessoa = value; }

        [Column("Cep")]
        public string Cep { get => cep; set => cep = value; }

        [Column("Numero")]
        public string Numero { get => numero; set => numero = value; }

        [Column("Rua")]
        public string Rua { get => rua; set => rua = value; }

        [Column("Complemento")]
        public string Complemento { get => complemento; set => complemento = value; }

        [Column("Bairro")]
        public string Bairro { get => bairro; set => bairro = value; }

        [Column("Cidade")]
        public string Cidade { get => cidade; set => cidade = value; }

        [Column("Estado")]
        public string Estado { get => estado; set => estado = value; }
		
		[Column("DataCadastro")]
        public DateTime DataCadastro { get => dataCadastro; set => dataCadastro = value; }

        public Pessoa Pessoa { get => pessoa; set => pessoa = value; }

        private Pessoa pessoa;
        private int id;
        private int idPessoa;
        private string rua;
        private string cep;
        private string numero;
        private string complemento;
        private string bairro;
        private string cidade;
        private string estado;
        private DateTime dataCadastro;

        public Endereco(int idPessoa, string rua, string cep, string numero, string complemento, string bairro, string cidade, string estado)
        {
            this.idPessoa = idPessoa;
            this.rua = rua;
            this.cep = cep;
            this.numero = numero;
            this.complemento = complemento;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
        }

        public void Cadastrar()
        {
            Contexto ItakaConnection = new Contexto();
            ItakaConnection.Enderecos.Add(this);
            ItakaConnection.SaveChanges();
            ItakaConnection.Dispose();
        }

        public void Editar()
        {
            Contexto ItakaConnection = new Contexto();
            ItakaConnection.Entry(this).State = EntityState.Modified;
            ItakaConnection.SaveChanges();
        }

        public void Excluir()
        {
            Contexto ItakaConnection = new Contexto();
            Endereco endereco = ItakaConnection.Enderecos.Find(Id);
            ItakaConnection.Enderecos.Remove(endereco);
            ItakaConnection.SaveChanges();
        }
    }
}
