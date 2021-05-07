using System;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaSolicitante : TelaBase
    {
        private ControladorSolicitante controladorSolicitante;
        public TelaSolicitante(ControladorSolicitante controlador)
        : base("Cadastro de Solicitantes")
        {
            controladorSolicitante = controlador;
        }
        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo solicitante...");

            bool conseguiuGravar = GravarSolicitante(0);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o solicitante", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }
        public override void EditarRegistro()
        {
            ConfigurarTela("Editando um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do solicitante que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarSolicitante(id);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o solicitante", TipoMensagem.Erro);
                EditarRegistro();
            }
        }
        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do solicitante que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorSolicitante.ExcluirSolicitante(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Solicitante excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o solicitante", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }
        public override void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando solicitantes...");

            string configuracaColunasTabela = "{0,-10} | {1,-30} | {2,-45} | {3,0}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Solicitante[] solicitante = controladorSolicitante.SelecionarTodosSolicitantes();

            if (solicitante.Length == 0)
            {
                ApresentarMensagem("Nenhum solicitante cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < solicitante.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   solicitante[i].id, solicitante[i].nome, solicitante[i].email, solicitante[i].numeroTelefone);
            }
        }
        private bool GravarSolicitante(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do solicitante: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email do solicitante: ");
            string email = Console.ReadLine();

            Console.Write("Digite o número de telefone: ");
            string numeroTelefone = Console.ReadLine();

            resultadoValidacao = controladorSolicitante.RegistrarSolicitante(
                id, nome, email, numeroTelefone);

            if (resultadoValidacao != "SOLICITANTE_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Nome", "Email", "Numero de Telefone");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }



    }
}
