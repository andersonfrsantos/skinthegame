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

                    IConfiguration configuration = new ConfigurationBuilder().SetBasePath("C:\\NetCore\\Projetos\\ukBets\\bin\\Debug\\netcoreapp3.1")
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
                    //IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
                    
                    Arquivos arquivo = new Util.Arquivos();
                    XmlDocument xml = new XmlDocument();
                    var validateModel = new ValidationModelsukBetsHistory();   
                    RepositoryukBetsHistory repositorioApostas = new RepositoryukBetsHistory();
                    DataTable DataTabletRetornoConsulta;
                    string NomeArquivoOrigem = configuration.GetSection("CaminhoXml").GetSection("Origem").Value;
                    string NomeArquivoDestino = configuration.GetSection("CaminhoXml").GetSection("Destino").Value;
                    
                    
                    Console.WriteLine("2 - Descompactando Arquivo .Gz");
                    // Descompactar arquivo .GZ
                    arquivo.ExtractGZ(NomeArquivoOrigem,NomeArquivoDestino);
                    
                    // Carrega Arquivo Xml
                    Console.WriteLine("3 - Carregando XML");
                    xml.Load(NomeArquivoDestino);            
                    
                    // Preenche Objeto ukBetsHistory
                    Console.WriteLine("4 - Transformando XML em Lista de memoria");
                    List<ukBetsHistory> BetsList = arquivo.PopulaObjetoukBetsHistory(xml);
                    
                    Console.WriteLine("5 - Inserindo no banco de dados SQL");
                    for (int i = 0;i < BetsList.Count; i++)
                    {
                        DataTabletRetornoConsulta = repositorioApostas.EfetuarConsultaPorSelectionUniqueID(BetsList[i].SelectionUniqueID.ToString(),BetsList[i].BetId.ToString());
                        if (DataTabletRetornoConsulta.Rows.Count==0)
                        {
                            BetsList[i].MatchedDate = BetsList[i].SettledDate;
                            var validaAntesInserir = validateModel.Validate(BetsList[i]);
                            
                            if (!validaAntesInserir.IsValid)
                            {
                                Console.WriteLine("Nao Consegui Inserir o BetId " + BetsList[i].BetId + " " + BetsList[i].StrategyName );
                            }
                            else
                            {
                                repositorioApostas.InserirBet(BetsList[i]);
                            }
                        }
                    }
                    Console.WriteLine("6 - Fim");

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
