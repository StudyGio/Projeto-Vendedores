using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVendedores.Models
{
    public class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public double PercComissao
        {
            get { return percComissao; }
            set { percComissao = value; }
        }

        public Vendedor(int id, string nome, double percComissao)
        {
            this.id = id;
            this.nome = nome;
            this.percComissao = percComissao;

            // Vetor para os 31 dias do mês
            asVendas = new Venda[31];
        }

        public void RegistrarVenda(int dia, Venda venda)
        {
            if (dia >= 1 && dia <= 31)
            {
                // Dia 1 = posição 0 do vetor
                asVendas[dia - 1] = venda;
            }
        }

        public double ValorVendas()
        {
            double total = 0;

            for (int i = 0; i < asVendas.Length; i++)
            {
                if (asVendas[i] != null)
                {
                    total += asVendas[i].Valor;
                }
            }

            return total;
        }

        public double ValorComissao()
        {
            return ValorVendas() * (percComissao / 100);
        }

        public int QuantidadeDiasComVenda()
        {
            int quantidade = 0;

            for (int i = 0; i < asVendas.Length; i++)
            {
                if (asVendas[i] != null)
                {
                    quantidade++;
                }
            }

            return quantidade;
        }

        public double ValorMedioVendasDiarias()
        {
            int diasComVenda = QuantidadeDiasComVenda();

            if (diasComVenda == 0)
                return 0;

            return ValorVendas() / diasComVenda;
        }

        public bool PossuiVendas()
        {
            return QuantidadeDiasComVenda() > 0;
        }
    }
}