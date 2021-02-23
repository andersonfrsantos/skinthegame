using System;
using System.Collections.Generic;
using System.Text;

namespace UKBET.Models
{
    public class ukBetsHistory
    {
        public string Exchange { get; set; }
        public string MarketId { get; set; }
        public string SelectionId { get; set; }
        public string Handicap { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public decimal Matched { get; set; }
        public decimal Unmatched { get; set; }
        public string Cancelled { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal PriceRequested { get; set; }
        public string BetType { get; set; }
        public string BetId { get; set; }
        public DateTime PlacedDate { get; set; }
        public DateTime StartTime { get; set; }
        public string StrategyName { get; set; }
        public double ProfitLoss { get; set; }
        public string ShortDescription { get; set; }
        public decimal LossRecovery { get; set; }
        public string Currency { get; set; }
        public string FullMarketName { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionId { get; set; }
        public string SelectionName { get; set; }
        public string MarketName { get; set; }
        public string EventTypeName { get; set; }
        public string EventTypeId { get; set; }
        public string MarketType { get; set; }
        public string MarketTypeVariant { get; set; }
        public string SimulatedBet { get; set; }
        public decimal TotalMatchedOnMarket { get; set; }
        public decimal TotalMatchedOnRunner { get; set; }
        public string NumberOfRunners { get; set; }
        public string FavoriteByPosition { get; set; }
        public string StrategyID { get; set; }
        public string SelectionUniqueID { get; set; }
        public string RunnerByPosition { get; set; }
        public DateTime MatchedDate { get; set; }
        public DateTime SettledDate { get; set; }
        public decimal LossRecoveryAmount { get; set; }
        public string PriceReduced { get; set; }
        public string PersistenceType { get; set; }
        public string OrderType { get; set; }
        public decimal Commission { get; set; }
        public decimal SizeCancelled { get; set; }
        public decimal SizeSettled { get; set; }
        public decimal SizeLapsed { get; set; }
        public decimal BSP { get; set; }
        public string Monitor { get; set; }
    }



}
