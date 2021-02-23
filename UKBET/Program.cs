using FluentAssertions.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Xml;
using UKBET.Models;
using UKBET.Repository;
using UKBET.Util;

namespace UKBET
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {

            Console.WriteLine("1 - Iniciando Copia de Apostas" + DateTime.Now);
            try
            {
                GetAppSettings();

                Arquivos arquivo = new Arquivos();
                XmlDocument xml = new XmlDocument();
                XmlDocument xml_b = new XmlDocument();

                var validateModel = new ValidationModelsukBetsHistory();
                RepositoryukBetsHistory repositorioApostas = new RepositoryukBetsHistory();
                DataTable DataTableRetornoConsulta = new DataTable();
                DataTable dtJogosEstrategia = new DataTable();
                DataTable dtEstrategiasAgrupadas = new DataTable();

                string NomeArquivoOrigem = Configuration.GetSection("CaminhoXml").GetSection("Origem").Value;
                string NomeArquivoDestino = Configuration.GetSection("CaminhoXml").GetSection("Destino").Value;
                string NomeArquivoOrigem_B = Configuration.GetSection("CaminhoXml").GetSection("Origem_B").Value;
                string NomeArquivoDestino_B = Configuration.GetSection("CaminhoXml").GetSection("Destino_B").Value;


                Console.WriteLine("2 - Descompactando Arquivo .Gz -> MONITOR A");
                arquivo.ExtractGZ(NomeArquivoOrigem, NomeArquivoDestino);

                Console.WriteLine("3 - Carregando XML -> MONITOR A");
                xml.Load(NomeArquivoDestino);

                Console.WriteLine("4 - Descompactando Arquivo .Gz -> MONITOR B");
                arquivo.ExtractGZ(NomeArquivoOrigem_B, NomeArquivoDestino_B);

                Console.WriteLine("5 - Carregando XML -> MONITOR B");
                xml_b.Load(NomeArquivoDestino_B);

                Console.WriteLine("6 - Transformando XML's A e B em Lista de memoria");
                List<ukBetsHistory> BetsList = arquivo.PopulaObjetoukBetsHistory(xml, "A");
                List<ukBetsHistory> BetsList_b = arquivo.PopulaObjetoukBetsHistory(xml_b,"B");

                Console.WriteLine("7 - Inserindo no banco de dados SQL MONITOR A");
                InserirBetsBancoDados("A",validateModel, repositorioApostas, DataTableRetornoConsulta, BetsList);

                Console.WriteLine("8 - Inserindo no banco de dados SQL MONITOR B");
                InserirBetsBancoDados("B",validateModel, repositorioApostas, DataTableRetornoConsulta, BetsList_b);

                repositorioApostas.TruncaTabelaUKBETSHISTORY_RECOVERY();
                
                Console.WriteLine("9 - Atualizando tabela de Recovery MONITOR A");
                dtEstrategiasAgrupadas = repositorioApostas.RetornaEstrategiaAgrupada();
                CriarTabelaRecovery(repositorioApostas, dtJogosEstrategia, dtEstrategiasAgrupadas);

            }

            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                Console.WriteLine("Processo Finalizado" + DateTime.Now);
            }
        }

        private static void CriarTabelaRecovery(RepositoryukBetsHistory repositorioApostas, DataTable dtJogosEstrategia, DataTable dtEstrategiasAgrupadas)
        {

            DataTable dtEstrategiasAgrupadas_b = new DataTable();
            dtEstrategiasAgrupadas_b = dtEstrategiasAgrupadas;


            for (int i = 0; i < dtEstrategiasAgrupadas.Rows.Count; i++)
            {

                dtJogosEstrategia = repositorioApostas.InsereRetornaJogosPorEstrategia(dtEstrategiasAgrupadas.Rows[i][0].ToString(), "A");

                int contadorPositivo = 0;
                int contadorNegativo = 0;

                for (int jogo = 0; jogo < dtJogosEstrategia.Rows.Count; jogo++)
                {

                    if (Convert.ToDecimal(dtJogosEstrategia.Rows[jogo][4]) < 0)
                    {
                        contadorPositivo = 0;
                        contadorNegativo = contadorNegativo - 1;
                        repositorioApostas.AtualizaContadorUKBETSHISTORY_RECOVERY(contadorNegativo, Convert.ToInt32(dtJogosEstrategia.Rows[jogo][0].ToString()));
                    }
                    else if (Convert.ToDecimal(dtJogosEstrategia.Rows[jogo][4]) > 0)
                    {
                        contadorNegativo = 0;
                        contadorPositivo = contadorPositivo + 1;
                        repositorioApostas.AtualizaContadorUKBETSHISTORY_RECOVERY(contadorPositivo, Convert.ToInt32(dtJogosEstrategia.Rows[jogo][0].ToString()));
                    }
                }

                //dtJogosEstrategia = repositorioApostas.InsereRetornaJogosPorEstrategia(dtEstrategiasAgrupadas_b.Rows[i][0].ToString(), "B");

                //contadorPositivo = 0;
                //contadorNegativo = 0;

                //for (int jogo = 0; jogo < dtJogosEstrategia.Rows.Count; jogo++)
                //{

                //    if (Convert.ToDecimal(dtJogosEstrategia.Rows[jogo][4]) < 0)
                //    {
                //        contadorPositivo = 0;
                //        contadorNegativo = contadorNegativo - 1;
                //        repositorioApostas.AtualizaContadorUKBETSHISTORY_RECOVERY(contadorNegativo, Convert.ToInt32(dtJogosEstrategia.Rows[jogo][0].ToString()));
                //    }
                //    else if (Convert.ToDecimal(dtJogosEstrategia.Rows[jogo][4]) > 0)
                //    {
                //        contadorNegativo = 0;
                //        contadorPositivo = contadorPositivo + 1;
                //        repositorioApostas.AtualizaContadorUKBETSHISTORY_RECOVERY(contadorPositivo, Convert.ToInt32(dtJogosEstrategia.Rows[jogo][0].ToString()));
                //    }
                //}
            }
        }

        private static void InserirBetsBancoDados(string pMonitor, ValidationModelsukBetsHistory validateModel, RepositoryukBetsHistory repositorioApostas, DataTable DataTableRetornoConsulta, List<ukBetsHistory> BetsList)
        {
            Console.WriteLine("Truncar tabela UKBETSHISTORY_TMP");
            repositorioApostas.TruncarTabelaUKBETSHISTORY_TMP();

            Console.WriteLine("Gravando Tabela Temporaria Monitor " +pMonitor );
            for (int i = 0; i < BetsList.Count; i++)
            {
                GravarBetTmp(validateModel, repositorioApostas, BetsList, i);
            }
            // Grava em Lote
            GravarBetUKBETSHISTORY(pMonitor, repositorioApostas);
            Console.WriteLine("Lote gravado " + pMonitor);
        }

        private static void GravarBetTmp(ValidationModelsukBetsHistory validateModel, RepositoryukBetsHistory repositorioApostas, List<ukBetsHistory> BetsList, int i)
        {

            BetsList[i].MatchedDate = BetsList[i].SettledDate;
            var validaAntesInserir = validateModel.Validate(BetsList[i]);

            if (!validaAntesInserir.IsValid)
            {
                Console.WriteLine("Nao Consegui Inserir o BetId " + BetsList[i].BetId + " " + BetsList[i].StrategyName);
            }
            else
            {
                BetsList[i].ProfitLoss = BetsList[i].ProfitLoss * 0.92;
                repositorioApostas.InserirBetTemp(BetsList[i]);
            }
        }

        private static void GravarBetUKBETSHISTORY(string pMonitor, RepositoryukBetsHistory repositorioApostas)
        {
            repositorioApostas.InserirBet(pMonitor);
        }

        private static void GetAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
    }
}
