using System;
using ProjetoVendedores.Models;

namespace ProjetoVendedores
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Vendedores vendedores = new Vendedores(10);

            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("      SISTEMA DE VENDEDORES");
                Console.WriteLine("=================================");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("1 - Cadastrar vendedor");
                Console.WriteLine("2 - Consultar vendedor");
                Console.WriteLine("3 - Excluir vendedor");
                Console.WriteLine("4 - Registrar venda");
                Console.WriteLine("5 - Listar vendedores");
                Console.WriteLine("=================================");

                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Sistema encerrado!");
                        break;

                    case 1:
                        CadastrarVendedor(vendedores);
                        break;

                    case 2:
                        ConsultarVendedor(vendedores);
                        break;

                    case 3:
                        ExcluirVendedor(vendedores);
                        break;

                    case 4:
                        RegistrarVenda(vendedores);
                        break;

                    case 5:
                        ListarVendedores(vendedores);
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }

            } while (opcao != 0);
        }


        static void CadastrarVendedor(Vendedores vendedores)
        {
            Console.Clear();

            Console.WriteLine("=== CADASTRAR VENDEDOR ===");

            Console.Write("ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            
            if (vendedores.SearchVendedor(id) != null)
            {
                Console.WriteLine("Já existe um vendedor com esse ID!");
                Console.ReadKey();
                return;
            }

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Percentual de comissão: ");

            if (!double.TryParse(Console.ReadLine(), out double comissao))
            {
                Console.WriteLine("Percentual inválido!");
                Console.ReadKey();
                return;
            }

            
            Vendedor vendedor = new Vendedor(id, nome, comissao);

            
            if (vendedores.AddVendedor(vendedor))
            {
                Console.WriteLine();
                Console.WriteLine("Vendedor cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Não foi possível cadastrar!");
                Console.WriteLine("Limite máximo de 10 vendedores atingido.");
            }

            Console.ReadKey();
        }


        static void ConsultarVendedor(Vendedores vendedores)
        {
            Console.Clear();

            Console.WriteLine("=== CONSULTAR VENDEDOR ===");

            Console.Write("Digite o ID do vendedor: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

           
            Vendedor vendedor = vendedores.SearchVendedor(id);

            if (vendedor == null)
            {
                Console.WriteLine();
                Console.WriteLine("Vendedor não encontrado!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("=================================");
                Console.WriteLine("DADOS DO VENDEDOR");
                Console.WriteLine("=================================");

                Console.WriteLine("ID: " + vendedor.Id);
                Console.WriteLine("Nome: " + vendedor.Nome);

                Console.WriteLine(
                    "Valor total das vendas: R$ " +
                    vendedor.ValorVendas().ToString("F2")
                );

                Console.WriteLine(
                    "Valor da comissão: R$ " +
                    vendedor.ValorComissao().ToString("F2")
                );

                Console.WriteLine(
                    "Valor médio das vendas diárias: R$ " +
                    vendedor.ValorMedioVendasDiarias().ToString("F2")
                );
            }

            Console.ReadKey();
        }


        static void ExcluirVendedor(Vendedores vendedores)
        {
            Console.Clear();

            Console.WriteLine("=== EXCLUIR VENDEDOR ===");

            Console.Write("Digite o ID do vendedor: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

            Vendedor vendedor = vendedores.SearchVendedor(id);

            if (vendedor == null)
            {
                Console.WriteLine();
                Console.WriteLine("Vendedor não encontrado!");
            }
            else if (vendedor.PossuiVendas())
            {
                Console.WriteLine();
                Console.WriteLine("Não é possível excluir este vendedor!");
                Console.WriteLine("O vendedor possui vendas registradas.");
            }
            else
            {
                if (vendedores.DelVendedor(id))
                {
                    Console.WriteLine();
                    Console.WriteLine("Vendedor excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Não foi possível excluir o vendedor!");
                }
            }

            Console.ReadKey();
        }


        static void RegistrarVenda(Vendedores vendedores)
        {
            Console.Clear();

            Console.WriteLine("=== REGISTRAR VENDA ===");

            Console.Write("ID do vendedor: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido!");
                Console.ReadKey();
                return;
            }

           
            Vendedor vendedor = vendedores.SearchVendedor(id);

            if (vendedor == null)
            {
                Console.WriteLine();
                Console.WriteLine("Vendedor não encontrado!");
                Console.ReadKey();
                return;
            }

            Console.Write("Dia da venda (1 a 31): ");

            if (!int.TryParse(Console.ReadLine(), out int dia))
            {
                Console.WriteLine("Dia inválido!");
                Console.ReadKey();
                return;
            }

            
            if (dia < 1 || dia > 31)
            {
                Console.WriteLine("O dia deve estar entre 1 e 31!");
                Console.ReadKey();
                return;
            }

            Console.Write("Quantidade de vendas: ");

            if (!int.TryParse(Console.ReadLine(), out int qtde))
            {
                Console.WriteLine("Quantidade inválida!");
                Console.ReadKey();
                return;
            }

            Console.Write("Valor total das vendas: R$ ");

            if (!double.TryParse(Console.ReadLine(), out double valor))
            {
                Console.WriteLine("Valor inválido!");
                Console.ReadKey();
                return;
            }
            
            Venda venda = new Venda(qtde, valor);

            vendedor.RegistrarVenda(dia, venda);

            Console.WriteLine();
            Console.WriteLine("Venda registrada com sucesso!");

            Console.ReadKey();
        }

        static void ListarVendedores(Vendedores vendedores)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("       LISTA DE VENDEDORES");
            Console.WriteLine("=================================");
            Console.WriteLine();
           
            Vendedor[] lista = vendedores.GetVendedores();

            int quantidade = vendedores.GetQuantidade();

            if (quantidade == 0)
            {
                Console.WriteLine("Nenhum vendedor cadastrado.");
            }
            else
            {
                for (int i = 0; i < quantidade; i++)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("ID: " + lista[i].Id);
                    Console.WriteLine("Nome: " + lista[i].Nome);

                    Console.WriteLine(
                        "Total de vendas: R$ " +
                        lista[i].ValorVendas().ToString("F2")
                    );

                    Console.WriteLine(
                        "Comissão: R$ " +
                        lista[i].ValorComissao().ToString("F2")
                    );
                }

                // Mostra os totais finais
                Console.WriteLine("=================================");
                Console.WriteLine("             TOTAIS");
                Console.WriteLine("=================================");

                Console.WriteLine(
                    "Total geral de vendas: R$ " +
                    vendedores.ValorVendas().ToString("F2")
                );

                Console.WriteLine(
                    "Total geral de comissões: R$ " +
                    vendedores.ValorComissao().ToString("F2")
                );
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    }
}