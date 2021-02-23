using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Api;
using Model;
using RestSharp;
using Newtonsoft.Json;


namespace WindowsFormsPE35
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnInicializa_Click(object sender, EventArgs e)
        {
            // Chamo a DLL para bater no EndPoint Pegar os Jogos do momento (Ao Vivo)
            ApiFootball _ApiFootball = new ApiFootball();
            IRestResponse FixturesInPlay = _ApiFootball.GetfixturesInPlay();
            string sContentFixturesInPlay = FixturesInPlay.Content;

            // Solicito o Modelo para a DLL Model
            JsonToModelConverter _ConverterParaModelo = new JsonToModelConverter();
            _ConverterParaModelo.ModeloFixtureInPlay(sContentFixturesInPlay);

            // Chamo a DLL para bater no EndPoint para pegar as Estatisticas de um jogo especifico
            IRestResponse FixturesStatistics = _ApiFootball.GetFixturesStatistics(328257);
            string sContentFixturesStatistics = FixturesStatistics.Content;

            // Solicito o Modelo para a DLL Model
            _ConverterParaModelo.ModeloFixtureStatistics(sContentFixturesStatistics);
            // Passo o Modelo para a Classe de Dados Gravar No Banco
        }
    }
}
