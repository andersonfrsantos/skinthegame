using System.Xml;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using ukBets.Models;

namespace ukBets.Util
{
    public class Arquivos
    {
        


        public void ExtractGZ(String gzArchiveName, String destFolder)
        {
            using (var inputFileStream = new FileStream(gzArchiveName, FileMode.Open))
            using (var gzipStream = new GZipStream(inputFileStream, CompressionMode.Decompress))
            using (var outputFileStream = new FileStream(destFolder, FileMode.Create))
            {
                gzipStream.CopyTo(outputFileStream);
            }
        }

        public List<ukBetsHistory> PopulaObjetoukBetsHistory(XmlDocument pXml)
        {
            List<ukBetsHistory> BetsList = new List<ukBetsHistory>();

            try
            {
                XmlNodeList xnList = pXml.GetElementsByTagName("BetHistoryItem");

                for (int i = 0; i < xnList.Count; i++)
                {
                    if (xnList[i]["Status"].InnerText == "SETTLED")
                    {
                        Models.ukBetsHistory Bet = new Models.ukBetsHistory()
                        {
                            //Exchange = xnList[i]["Exchange"].InnerText,
                            MarketId = xnList[i]["MarketId"].InnerText,
                            SelectionId = xnList[i]["SelectionId"].InnerText,
                            Handicap = xnList[i]["Handicap"].InnerText,
                            Name = xnList[i]["Name"].InnerText,
                            Status = xnList[i]["Status"].InnerText,
                            Matched = Convert.ToDecimal(xnList[i]["Matched"].InnerText),//.Replace(".",",")),
                            Unmatched = Convert.ToDecimal(xnList[i]["Unmatched"].InnerText),//.InnerText.Replace(".",",")),
                            Cancelled = xnList[i]["Cancelled"].InnerText,
                            AvgPrice = Convert.ToDecimal(xnList[i]["AvgPrice"].InnerText),//.InnerText.Replace(".",",")),     
                            PriceRequested = Convert.ToDecimal(xnList[i]["PriceRequested"].InnerText),//.InnerText.Replace(".",",")),
                            BetType = xnList[i]["BetType"].InnerText,
                            BetId = xnList[i]["BetId"].InnerText,
                            PlacedDate = Convert.ToDateTime(xnList[i]["PlacedDate"].InnerText),
                            StartTime = Convert.ToDateTime(xnList[i]["StartTime"].InnerText),
                            StrategyName = xnList[i]["StrategyName"].InnerText,
                            ProfitLoss = Convert.ToDecimal(xnList[i]["ProfitLoss"].InnerText),//.Replace(".",",")),
                            //ShortDescription = xnList[i]["ShortDescription"].InnerText,                    
                            LossRecovery = Convert.ToDecimal(xnList[i]["LossRecovery"].InnerText),//.Replace(".",",")),
                            Currency = xnList[i]["Currency"].InnerText,
                            //CountryCode = xnList[i]["CountryCode"].InnerText,
                            FullMarketName = xnList[i]["FullMarketName"].InnerText,
                            CompetitionName = xnList[i]["CompetitionName"].InnerText,
                            CompetitionId = xnList[i]["CompetitionId"].InnerText,
                            SelectionName = xnList[i]["SelectionName"].InnerText,
                            MarketName = xnList[i]["MarketName"].InnerText,
                            EventTypeName = xnList[i]["EventTypeName"].InnerText,
                            EventTypeId = xnList[i]["EventTypeId"].InnerText,                    
                            MarketType = xnList[i]["MarketType"].InnerText,
                            MarketTypeVariant = xnList[i]["MarketTypeVariant"].InnerText,
                            SimulatedBet = xnList[i]["SimulatedBet"].InnerText,
                            TotalMatchedOnMarket = Convert.ToDecimal(xnList[i]["TotalMatchedOnMarket"].InnerText),//.Replace(".",",")),
                            TotalMatchedOnRunner = Convert.ToDecimal(xnList[i]["TotalMatchedOnRunner"].InnerText),//.Replace(".",",")),
                            NumberOfRunners = xnList[i]["NumberOfRunners"].InnerText,                    
                            FavoriteByPosition = xnList[i]["FavoriteByPosition"].InnerText,
                            StrategyID = xnList[i]["StrategyID"].InnerText,
                            SelectionUniqueID = xnList[i]["SelectionUniqueID"].InnerText,
                            RunnerByPosition = xnList[i]["RunnerByPosition"].InnerText,
                            MatchedDate = Convert.ToDateTime(xnList[i]["MatchedDate"].InnerText),
                            SettledDate = Convert.ToDateTime(xnList[i]["SettledDate"].InnerText),
                            LossRecoveryAmount = Convert.ToDecimal(xnList[i]["LossRecoveryAmount"].InnerText),//.Replace(".",",")),
                            PriceReduced = xnList[i]["PriceReduced"].InnerText,
                            PersistenceType = xnList[i]["PersistenceType"].InnerText,                    
                            OrderType = xnList[i]["OrderType"].InnerText,
                            Commission = Convert.ToDecimal(xnList[i]["Commission"].InnerText),//.Replace(".",",")),                    
                            SizeCancelled = Convert.ToDecimal(xnList[i]["SizeCancelled"].InnerText),//.Replace(".",",")), 
                            SizeSettled = Convert.ToDecimal(xnList[i]["SizeSettled"].InnerText),//.Replace(".",",")), 
                            SizeLapsed = Convert.ToDecimal(xnList[i]["SizeLapsed"].InnerText),//.Replace(".",",")), 
                            BSP = Convert.ToDecimal(xnList[i]["BSP"].InnerText),//.Replace(".",","))
                        }; 

                        BetsList.Add(Bet);
                    }
                    //if (BetsList.Count == 1388)
                    //{

                    //Console.WriteLine("1388" + i.ToString());
                    //}
                }
                return BetsList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " " + BetsList.Count.ToString());
            }
        } 
    }
}