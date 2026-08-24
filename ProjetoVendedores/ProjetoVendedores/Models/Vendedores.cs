using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVendedores.Models
{
    public class Vendedores
    {
        private Vendedor[] osVendedores;
        private int max;
        private int qtde;

        public Vendedores(int max)
        {
            this.max = max;
            this.qtde = 0;

            osVendedores = new Vendedor[max];
        }

        public bool AddVendedor(Vendedor v)
        {
            // Verifica se chegou ao máximo
            if (qtde >= max)
                return false;

            // Verifica se o ID já existe
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == v.Id)
                    return false;
            }

            osVendedores[qtde] = v;
            qtde++;

            return true;
        }

        public Vendedor SearchVendedor(int id)
        {
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == id)
                {
                    return osVendedores[i];
                }
            }

            return null;
        }

        public bool DelVendedor(int id)
        {
            int posicao = -1;

            // Procura a posição do vendedor
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == id)
                {
                    posicao = i;
                    break;
                }
            }

            // Não encontrou
            if (posicao == -1)
                return false;

            // Não pode excluir se tiver vendas
            if (osVendedores[posicao].PossuiVendas())
                return false;

            // Move os vendedores uma posição para trás
            for (int i = posicao; i < qtde - 1; i++)
            {
                osVendedores[i] = osVendedores[i + 1];
            }

            osVendedores[qtde - 1] = null;
            qtde--;

            return true;
        }

        public Vendedor[] GetVendedores()
        {
            return osVendedores;
        }

        public int GetQuantidade()
        {
            return qtde;
        }

        public double ValorVendas()
        {
            double total = 0;

            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].ValorVendas();
            }

            return total;
        }

        public double ValorComissao()
        {
            double total = 0;

            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].ValorComissao();
            }

            return total;
        }
    }
}