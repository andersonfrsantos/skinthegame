using System.Linq.Expressions;
using System.Xml;
using System;
using System.Collections.Generic;
using ukBets.Models;
using ukBets.Util;
using ukBets.Repository;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ukBets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - Iniciando Copia de Apostas");
            try
            {
                IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();      

                Arquivos arquivo = new Util.Arquivos();
                XmlDocument xml = new XmlDocument();
                var validateModel = new ValidationModelsukBetsHistory();
                RepositoryukBetsHistory repositorioApostas = new RepositoryukBetsHistory();

                DataTable DataTableRetornoConsulta = new DataTable();
                DataTable dtJogosEstrategia = new DataTable();
                DataTable dtEstrategiasAgrupadas = new DataTable();

                string NomeArquivoOrigem = configuration.GetSection("CaminhoXml").GetSection("Origem").Value;
                string NomeArquivoDestino = configuration.GetSection("CaminhoXml").GetSection("Destino").Value;

                Console.WriteLine("2 - Descompactando Arquivo .Gz");
                arquivo.ExtractGZ(NomeArquivoOrigem, NomeArquivoDestino);

                Console.WriteLine("3 - Carregando XML");
                xml.Load(NomeArquivoDestino);

                Console.WriteLine("4 - Transformando XML em Lista de memoria");
                List<ukBetsHistory> BetsList = arquivo.PopulaObjetoukBetsHistory(xml);

                Console.WriteLine("5 - Inserindo no banco de dados SQL");
                for (int i = 0; i < BetsList.Count; i++)
                {
                    DataTableRetornoConsulta = repositorioApostas.EfetuarConsultaPorSelectionUniqueID(BetsList[i].SelectionUniqueID.ToString(), BetsList[i].BetId.ToString());
                    if (DataTableRetornoConsulta.Rows.Count == 0)
                    {
                        BetsList[i].MatchedDate = BetsList[i].SettledDate;
                        var validaAntesInserir = validateModel.Validate(BetsList[i]);

                        if (!validaAntesInserir.IsValid)
                        {
                            Console.WriteLine("Nao Consegui Inserir o BetId " + BetsList[i].BetId + " " + BetsList[i].StrategyName);
                        }
                        else
                        {
                            BetsList[i].ProfitLoss = BetsList[i].ProfitLoss * 0.935;
                            repositorioApostas.InserirBet(BetsList[i]);
                        }
                    }
                }
                Console.WriteLine("6 - Atualizando tabela de Recovery");
                repositorioApostas.TruncaTabelaUKBETSHISTORY_RECOVERY();
                dtEstrategiasAgrupadas = repositorioApostas.RetornaEstrategiaAgrupada();

                for (int i = 0; i < dtEstrategiasAgrupadas.Rows.Count; i++)
                {
                    
                    dtJogosEstrategia = repositorioApostas.InsereRetornaJogosPorEstrategia(dtEstrategiasAgrupadas.Rows[i][0].ToString());

                    int contadorPositivo = 0;
                    int contadorNegativo = 0;

                    for (int jogo = 0; jogo < dtJogosEstrategia.Rows.Count; jogo++)
                    {

                        if (Convert.ToDecimal(dtJogosEstrategia.Rows[jogo][4]) < 0)
                        {
                            contadorPositivo = 0;
                            contadorNegativo = contadorNegativo - 1;
                            repositorioApostas.AtualizaContadorUKBETSHISTORY_RECOVERY(contadorNegativo,Convert.ToInt32(dtJogosEstrategia.Rows[jogo][0].ToString()));
                        }
                        else if (Convert.ToDecimal(dtJogosEstrategia.Rows[jogo][4]) > 0)
                        {
                            contadorNegativo = 0;
                            contadorPositivo = contadorPositivo + 1;
                            repositorioApostas.AtualizaContadorUKBETSHISTORY_RECOVERY(contadorPositivo,Convert.ToInt32(dtJogosEstrategia.Rows[jogo][0].ToString()));
                        }
                    }
                }




                // 7 - Incluir código para gerar tabela de Juros sobre juros.

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                // montar tabela de recovery repetições
            }

        }
    }
}
