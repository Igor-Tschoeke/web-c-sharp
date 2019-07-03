using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EstoqueRepositório
    {
        //Atraves do objeto da conexão
        //poderá ter acesso ao SqlCommand
        //de uma forma mais fácil
        Conexão conexao = new Conexão();

        public int Inserir(Estoque estoque)
        {
            //Insere no banco de dados
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO estoques(nome, valor, quantidade)VALUS(@NOME,@VALOR,@QUANTIDADE)";
            //Substitui as variaveis do comando acima
            comando.Parameters.AddWithValue("@NOME", estoque.Nome);
            comando.Parameters.AddWithValue("@VALOR", estoque.Valor);
            comando.Parameters.AddWithValue("@QUANTIDADE", estoque.Quantidade);
            //Executa o comando do BD e obtem o id que foi gerado automaticamente pela tabela
            //ExecuteScalar: executa um comando no BD e obtem uma informação.
            int id = Convert.ToInt32(comando.ExecuteScalar());
            //Fecha a conexão com o BD;
            comando.Connection.Close();
            //Retorna o ID que foi gerado no BD;
            return id;
        }

        public List<Estoque> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"SELCT * FROM estoques WHERE nome LIKE @NOME";
            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            List<Estoque> estoques = new List<Estoque>();
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Estoque estoque = new Estoque();

                estoque.Id = Convert.ToInt32(linha["Id"]);
                estoque.Nome = linha["Nome"].ToString();
                estoque.Valor = Convert.ToDecimal(linha["Valor"]);
                estoque.Quantidade = Convert.ToInt32(linha["Quantidade"]);
                estoques.Add(estoque);

            }
            
        }
        

    }
}
