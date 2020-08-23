using System.Data;
using System;

namespace ukBets.Repository
{
    public class RepositoryukBetsHistory
    {
        Data.DataAccess query = new Data.DataAccess();
        public bool InserirBet(Models.ukBetsHistory Bet)
        {
            // Limpa os parâmetros existente
            query.LimparParametros();
            string SQL = @"INSERT INTO ukBetsHistory
                ([Exchange], [MarketId], [SelectionId], [Handicap],[Name],[Status],[Matched],[Unmatched],[Cancelled],[AvgPrice],[PriceRequested],[BetType],
                [BetId],[PlacedDate],[StartTime],[StrategyName],[ProfitLoss],[LossRecovery],[Currency],[FullMarketName],[CompetitionName],[CompetitionId],
                [SelectionName],[MarketName],[EventTypeName],[EventTypeId],[MarketType],[MarketTypeVariant],[SimulatedBet],[TotalMatchedOnMarket],
                [TotalMatchedOnRunner],[NumberOfRunners],[FavoriteByPosition],[StrategyID],[SelectionUniqueID],[RunnerByPosition],[MatchedDate],[SettledDate],
                [LossRecoveryAmount],[PriceReduced],[PersistenceType],[OrderType],[Commission],[SizeCancelled],[SizeSettled],[SizeLapsed],[BSP])
                VALUES
                (@Exchange,@MarketId,@SelectionId,@Handicap,@Name,@Status,@Matched,@Unmatched,@Cancelled,@AvgPrice,@PriceRequested,@BetType,
                @BetId,@PlacedDate,@StartTime,@StrategyName,@ProfitLoss,@LossRecovery,@Currency,@FullMarketName,@CompetitionName,@CompetitionId,
                @SelectionName,@MarketName,@EventTypeName,@EventTypeId,@MarketType,@MarketTypeVariant,@SimulatedBet,@TotalMatchedOnMarket,
                @TotalMatchedOnRunner,@NumberOfRunners,@FavoriteByPosition,@StrategyID,@SelectionUniqueID,@RunnerByPosition,@MatchedDate,@SettledDate,
                @LossRecoveryAmount,@PriceReduced,@PersistenceType,@OrderType,@Commission,@SizeCancelled,@SizeSettled,@SizeLapsed,@BSP)";
            // Adiciona os parâmetros da instrução SQL
            query.AdicionarParametro("@Exchange", SqlDbType.VarChar, "UK");
            query.AdicionarParametro("@MarketId", SqlDbType.VarChar, Bet.MarketId);
            query.AdicionarParametro("@SelectionId", SqlDbType.VarChar, Bet.SelectionId);
            query.AdicionarParametro("@Handicap", SqlDbType.VarChar, Bet.Handicap);
            query.AdicionarParametro("@Name", SqlDbType.VarChar, Bet.Name);
            query.AdicionarParametro("@Status", SqlDbType.VarChar, Bet.Status);
            query.AdicionarParametro("@Matched", SqlDbType.Decimal, Math.Round(Bet.Matched,2));
            query.AdicionarParametro("@Unmatched", SqlDbType.Decimal, Bet.Unmatched);
            query.AdicionarParametro("@Cancelled", SqlDbType.VarChar, Bet.Cancelled);
            query.AdicionarParametro("@AvgPrice", SqlDbType.Decimal, Bet.AvgPrice);
            query.AdicionarParametro("@PriceRequested", SqlDbType.Decimal, Bet.PriceRequested);
            query.AdicionarParametro("@BetType", SqlDbType.VarChar, Bet.BetType);

            query.AdicionarParametro("@BetId", SqlDbType.VarChar, Bet.BetId);
            query.AdicionarParametro("@PlacedDate", SqlDbType.DateTime, Bet.PlacedDate);
            query.AdicionarParametro("@StartTime", SqlDbType.DateTime, Bet.StartTime);
            query.AdicionarParametro("@StrategyName", SqlDbType.VarChar, Bet.StrategyName);
            query.AdicionarParametro("@ProfitLoss", SqlDbType.Decimal, Bet.ProfitLoss);
            query.AdicionarParametro("@LossRecovery", SqlDbType.Decimal, Bet.LossRecovery);
            query.AdicionarParametro("@Currency", SqlDbType.VarChar, Bet.Currency);
            query.AdicionarParametro("@FullMarketName", SqlDbType.VarChar, Bet.FullMarketName);
            query.AdicionarParametro("@CompetitionName", SqlDbType.VarChar, Bet.CompetitionName);
            query.AdicionarParametro("@CompetitionId", SqlDbType.VarChar, Bet.CompetitionId);

            query.AdicionarParametro("@SelectionName", SqlDbType.VarChar, Bet.SelectionId);
            query.AdicionarParametro("@MarketName", SqlDbType.VarChar, Bet.MarketName);
            query.AdicionarParametro("@EventTypeName", SqlDbType.VarChar, Bet.EventTypeName);
            query.AdicionarParametro("@EventTypeId", SqlDbType.VarChar, Bet.EventTypeId);
            query.AdicionarParametro("@MarketType", SqlDbType.VarChar, Bet.MarketType);
            query.AdicionarParametro("@MarketTypeVariant", SqlDbType.VarChar, Bet.MarketTypeVariant);
            query.AdicionarParametro("@SimulatedBet", SqlDbType.VarChar, Bet.SimulatedBet);
            query.AdicionarParametro("@TotalMatchedOnMarket", SqlDbType.Decimal, Bet.TotalMatchedOnMarket);
            query.AdicionarParametro("@TotalMatchedOnRunner", SqlDbType.Decimal, Bet.TotalMatchedOnRunner);
            query.AdicionarParametro("@NumberOfRunners", SqlDbType.VarChar, Bet.NumberOfRunners);
            query.AdicionarParametro("@FavoriteByPosition", SqlDbType.VarChar, Bet.FavoriteByPosition);
            query.AdicionarParametro("@StrategyID", SqlDbType.VarChar, Bet.StrategyID);
            query.AdicionarParametro("@SelectionUniqueID", SqlDbType.VarChar, Bet.SelectionUniqueID);
            query.AdicionarParametro("@RunnerByPosition", SqlDbType.VarChar, Bet.RunnerByPosition);
            query.AdicionarParametro("@MatchedDate", SqlDbType.DateTime, Bet.MatchedDate);
            query.AdicionarParametro("@SettledDate", SqlDbType.DateTime, Bet.SettledDate);

            query.AdicionarParametro("@LossRecoveryAmount", SqlDbType.Decimal, Bet.LossRecoveryAmount);
            query.AdicionarParametro("@PriceReduced", SqlDbType.VarChar, Bet.PriceReduced);
            query.AdicionarParametro("@PersistenceType", SqlDbType.VarChar, Bet.PersistenceType);
            query.AdicionarParametro("@OrderType", SqlDbType.VarChar, Bet.OrderType);
            query.AdicionarParametro("@Commission", SqlDbType.Decimal, Bet.Commission);
            query.AdicionarParametro("@SizeCancelled", SqlDbType.Decimal, Math.Round(Bet.SizeCancelled,2));
            query.AdicionarParametro("@SizeSettled", SqlDbType.Decimal, Math.Round(Bet.SizeSettled,2));
            query.AdicionarParametro("@SizeLapsed", SqlDbType.Decimal, Bet.SizeLapsed);
            query.AdicionarParametro("@BSP", SqlDbType.Decimal, Bet.BSP);
            // Retorna a quantidade de linhas afetadas
            return (query.ExecutaAtualizacao(SQL) > 0);
        }


        public DataTable EfetuarConsultaPorSelectionUniqueID(string pSelectionUniqueID, string pBetid)
            {
                // Limpando parãmetros existentes
                query.LimparParametros();
                string SQL = @" SELECT * FROM UKBETSHISTORY 
                                WHERE SelectionUniqueID = @SelectionUniqueID
                                AND Betid = @Betid ";
                // Adicionando o parâmetro para filtrar pelo código
                query.AdicionarParametro("@SelectionUniqueID", SqlDbType.VarChar, pSelectionUniqueID);
                query.AdicionarParametro("@Betid", SqlDbType.VarChar, pBetid);
                // Retorna um DataTable com os dados da consulta
                return query.ExecutaConsulta(SQL);
            }




    }
}