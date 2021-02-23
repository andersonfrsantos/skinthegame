using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UKBET.Repository
{
    public class RepositoryukBetsHistory
    {

        Data.DataAccess query = new Data.DataAccess();
        public bool InserirBet(string pMonitor)
        {
            // Limpa os parâmetros existente
            query.LimparParametros();
            string SQL = @"INSERT INTO UKBETSHISTORY
                        SELECT Exchange,MarketId,SelectionId,Handicap,Name,Status,Matched,Unmatched,Cancelled,AvgPrice,PriceRequested,BetType,BetId,PlacedDate,StartTime,
                        StrategyName,ProfitLoss,LossRecovery,Currency,FullMarketName,CompetitionName,CompetitionId,SelectionName,MarketName,EventTypeName,
                        EventTypeId,MarketType,MarketTypeVariant,SimulatedBet,TotalMatchedOnMarket,TotalMatchedOnRunner,NumberOfRunners,FavoriteByPosition,
                        StrategyID,SelectionUniqueID,RunnerByPosition,MatchedDate,SettledDate,LossRecoveryAmount,PriceReduced,PersistenceType,OrderType,
                        Commission,SizeCancelled,SizeSettled,SizeLapsed,BSP,{Monitor_parametro} [Monitor]
                        FROM UKBETSHISTORY_TMP 
                        WHERE BetId  not in (select BetId from UKBETSHISTORY where Monitor = {Monitor_parametro} )";

            SQL = SQL.Replace("{Monitor_parametro}","'" + pMonitor + "'");
            return (query.ExecutaAtualizacao(SQL) > 0);
        }

        public bool InserirBetTemp(Models.ukBetsHistory Bet)
        {
            // Limpa os parâmetros existente
            query.LimparParametros();

            string SQL = @"INSERT INTO UKBETSHISTORY_TMP
                ([Exchange], [MarketId], [SelectionId], [Handicap],[Name],[Status],[Matched],[Unmatched],[Cancelled],[AvgPrice],[PriceRequested],[BetType],
                [BetId],[PlacedDate],[StartTime],[StrategyName],[ProfitLoss],[LossRecovery],[Currency],[FullMarketName],[CompetitionName],[CompetitionId],
                [SelectionName],[MarketName],[EventTypeName],[EventTypeId],[MarketType],[MarketTypeVariant],[SimulatedBet],[TotalMatchedOnMarket],
                [TotalMatchedOnRunner],[NumberOfRunners],[FavoriteByPosition],[StrategyID],[SelectionUniqueID],[RunnerByPosition],[MatchedDate],[SettledDate],
                [LossRecoveryAmount],[PriceReduced],[PersistenceType],[OrderType],[Commission],[SizeCancelled],[SizeSettled],[SizeLapsed],[BSP],[Monitor])
                VALUES
                (@Exchange,@MarketId,@SelectionId,@Handicap,@Name,@Status,@Matched,@Unmatched,@Cancelled,@AvgPrice,@PriceRequested,@BetType,
                @BetId,@PlacedDate,@StartTime,@StrategyName,@ProfitLoss,@LossRecovery,@Currency,@FullMarketName,@CompetitionName,@CompetitionId,
                @SelectionName,@MarketName,@EventTypeName,@EventTypeId,@MarketType,@MarketTypeVariant,@SimulatedBet,@TotalMatchedOnMarket,
                @TotalMatchedOnRunner,@NumberOfRunners,@FavoriteByPosition,@StrategyID,@SelectionUniqueID,@RunnerByPosition,@MatchedDate,@SettledDate,
                @LossRecoveryAmount,@PriceReduced,@PersistenceType,@OrderType,@Commission,@SizeCancelled,@SizeSettled,@SizeLapsed,@BSP,@Monitor)";
            // Adiciona os parâmetros da instrução SQL
            query.AdicionarParametro("@Exchange", SqlDbType.VarChar, "UK");
            query.AdicionarParametro("@MarketId", SqlDbType.VarChar, Bet.MarketId);
            query.AdicionarParametro("@SelectionId", SqlDbType.VarChar, Bet.SelectionId);
            query.AdicionarParametro("@Handicap", SqlDbType.VarChar, Bet.Handicap);
            query.AdicionarParametro("@Name", SqlDbType.VarChar, Bet.Name);
            query.AdicionarParametro("@Status", SqlDbType.VarChar, Bet.Status);
            query.AdicionarParametro("@Matched", SqlDbType.Decimal, Math.Round(Bet.Matched, 2));
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
            query.AdicionarParametro("@TotalMatchedOnMarket", SqlDbType.Decimal, Math.Round(Bet.TotalMatchedOnMarket, 2) / 100);
            query.AdicionarParametro("@TotalMatchedOnRunner", SqlDbType.Decimal, Math.Round(Bet.TotalMatchedOnRunner, 2) / 100);
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
            query.AdicionarParametro("@SizeCancelled", SqlDbType.Decimal, Math.Round(Bet.SizeCancelled, 2));
            query.AdicionarParametro("@SizeSettled", SqlDbType.Decimal, Math.Round(Bet.SizeSettled, 2));
            query.AdicionarParametro("@SizeLapsed", SqlDbType.Decimal, Bet.SizeLapsed);
            query.AdicionarParametro("@BSP", SqlDbType.Decimal, Bet.BSP);
            query.AdicionarParametro("@Monitor", SqlDbType.Char, Bet.Monitor);
            // Retorna a quantidade de linhas afetadas
            return (query.ExecutaAtualizacao(SQL) > 0);
        }
        public DataTable EfetuarConsultaPorSelectionUniqueID(string pSelectionUniqueID, string pBetid)
        {
            // Limpando parametros existentes
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


        public void TruncarTabelaUKBETSHISTORY_TMP()
        {
            // Limpa os parâmetros existente
            query.LimparParametros();
            string SQL = "TRUNCATE TABLE UKBETSHISTORY_TMP";
            query.ExecutaAtualizacao(SQL);

        }


        


        public DataTable RetornaEstrategiaAgrupada()
        {
            // Limpando parãmetros existentes
            query.LimparParametros();
            string SQL = @" SELECT StrategyID, 
	                                   StrategyName, Monitor
                                FROM UKBETSHISTORY
                                where Monitor = 'A'
                                group by StrategyID, StrategyName, Monitor
                                order by 3,2";
            // Adicionando o parâmetro para filtrar pelo código
            return query.ExecutaConsulta(SQL);
        }
        public DataTable InsereRetornaJogosPorEstrategia(string pStrategyID, string pMonitor)
        {
            // Limpando parãmetros existentes
            string SQL = "";
            query.LimparParametros();

            // Insere na Tabela
            SQL = @" INSERT UKBETSHISTORY_RECOVERY
                                SELECT  Id,
                                PlacedDate,
		                        StrategyID,
		                        StrategyName,
		                        ProfitLoss,
		                        Name,
		                        0 [Contador],
                                Monitor
                                from UKBETSHISTORY
                                where StrategyID = @StrategyID
                                and Monitor = @Monitor
                                and Status = 'SETTLED'
                                order by PlacedDate ";
            // Adicionando o parâmetro para filtrar pela estratégia
            query.AdicionarParametro("@StrategyID", SqlDbType.VarChar, pStrategyID);
            query.AdicionarParametro("@Monitor", SqlDbType.Char, pMonitor);
            query.ExecutaAtualizacao(SQL);

            query.LimparParametros();
            // Retorna um DataTable com os dados da consulta
            SQL = @"        select  Id,
                                PlacedDate,
		                        StrategyID,
		                        StrategyName,
		                        ProfitLoss,
		                        Name,
		                        0 [Contador],
                                Monitor
                                from UKBETSHISTORY
                                where StrategyID = @StrategyIDB
                                and Monitor = @Monitor
                                and Status = 'SETTLED'
                                order by PlacedDate ";
            // Adicionando o parâmetro para filtrar pela estratégia
            query.AdicionarParametro("@StrategyIDB", SqlDbType.VarChar, pStrategyID);
            query.AdicionarParametro("@Monitor", SqlDbType.Char, pMonitor);
            return query.ExecutaConsulta(SQL);
        }

        public void AtualizaContadorUKBETSHISTORY_RECOVERY(Int32 pValorContador, Int32 pId)
        {
            // Limpando parãmetros existentes
            query.LimparParametros();
            string SQL = @" UPDATE UKBETSHISTORY_RECOVERY
                                SET Contador = @Contador
                                WHERE ID = @Id ";
            // Adicionando o parâmetro para filtrar pela estratégia
            query.AdicionarParametro("@Contador", SqlDbType.Int, pValorContador);
            query.AdicionarParametro("@Id", SqlDbType.Int, pId);
            query.ExecutaAtualizacao(SQL);
        }

        public void TruncaTabelaUKBETSHISTORY_RECOVERY()
        {
            query.LimparParametros();
            string SQL = @"TRUNCATE TABLE UKBETSHISTORY_RECOVERY";
            query.ExecutaAtualizacao(SQL);
        }

    }
}
