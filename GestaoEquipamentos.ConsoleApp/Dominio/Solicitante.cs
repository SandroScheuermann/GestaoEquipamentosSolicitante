﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEquipamentos.ConsoleApp.Dominio
{
    public class Solicitante
    {
        public string nome = "";
        public string email = "";
        public string numeroTelefone = "";
        public int id;

        public Solicitante()
        {
            id = GeradorId.GerarIdSolicitante();
        }
        public Solicitante(int idSelecionado)
        {
            id = idSelecionado;
        }
        public string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(nome))
                resultadoValidacao += "O campo Nome é obrigatório \n";

            if (nome.Length < 6)
                resultadoValidacao += "O campo Nome não pode ter menos de 6 letras \n";

            if (string.IsNullOrEmpty(email))
                resultadoValidacao += "O campo email é obrigatório \n";

            if (string.IsNullOrEmpty(numeroTelefone))
                resultadoValidacao += "O campo número de telefone é obrigatório \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "SOLICITANTE_VALIDO";

            return resultadoValidacao;
        }
        public override bool Equals(object obj)
        {
            Solicitante solicitante = (Solicitante)obj;

            if (id == solicitante.id)
                return true;
            else
                return false;
        }

    }
}
