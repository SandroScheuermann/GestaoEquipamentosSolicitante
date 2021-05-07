using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaChamado : TelaBase
    {
        private TelaEquipamento telaEquipamento;
        private TelaSolicitante telaSolicitante;
        private ControladorChamado controladorChamado;

        public TelaChamado(TelaEquipamento telaE,TelaSolicitante telaS, ControladorChamado controlador)
            : base("Cadastro de Chamados")
        {
            telaEquipamento = telaE;
            telaSolicitante = telaS;
            controladorChamado = controlador;
        }
        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo chamado...");

            bool conseguiuGravar = GravarChamado(0);

            if (conseguiuGravar)
                ApresentarMensagem("Chamado inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o chamado", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }
        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um chamado...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do chamado que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorChamado.ExcluirChamado(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Chamado excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o chamado", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }
        public override void EditarRegistro()
        {
            ConfigurarTela("Editando um chamado...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do chamado que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuEditar = GravarChamado(idSelecionado);

            if (conseguiuEditar)
                ApresentarMensagem("Chamado editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o chamado", TipoMensagem.Erro);
                EditarRegistro();
            }
        }
        public override void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando chamados...");

            MontarCabecalhoTabela();

            Chamado[] chamados = controladorChamado.SelecionarTodosChamados();

            if (chamados.Length == 0)
            {
                ApresentarMensagem("Nenhum chamado registrado!", TipoMensagem.Atencao);
                return;
            }

            foreach (Chamado chamado in chamados)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20} | {4,-10}",
                    chamado.id, chamado.solicitante.nome, chamado.equipamento.nome, chamado.titulo, chamado.DiasEmAberto);
            }
        }

        #region Métodos Privados
        private static void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20} | {4,-10}", "Id", "Solicitante", "Equipamento", "Título", "Dias em Aberto");

            Console.WriteLine("--------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarChamado(int idChamadoSelecionado)
        {
            telaEquipamento.VisualizarRegistros();

            Console.Write("\nDigite o Id do equipamento para manutenção: \n");
            int idEquipamentoChamado = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            telaSolicitante.VisualizarRegistros();

            Console.Write("\nDigite o Id do solicitante da manutenção: \n");
            int idSolicitanteChamado = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            Console.Write("Digite o titulo do chamado: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descricao do chamado: ");
            string descricao = Console.ReadLine();

            Console.Write("Digite a data de abertura do chamado: ");
            DateTime dataAbertura = Convert.ToDateTime(Console.ReadLine());

            string resultadoValidacao = controladorChamado.
                RegistrarChamado(idChamadoSelecionado, idEquipamentoChamado,idSolicitanteChamado, titulo, descricao, dataAbertura);

            bool conseguiuGravar = true;

            if (resultadoValidacao != "CHAMADO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        #endregion
    }
}
