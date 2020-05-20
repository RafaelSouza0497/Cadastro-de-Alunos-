using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroItaka.Modelos.Pessoas
{
    public interface IPapel
    {
        bool Cadastrar();
        bool Editar();
        bool Excluir();
    }
}
 