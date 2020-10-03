using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace UKBET.Data
{
    public class DataAccess
    {

        // Objeto Connection para obter acesso ao SQL Server
        public static SqlConnection sqlconnection = new SqlConnection();
        // Objeto SqlCommand para executar os com
        public static SqlCommand comando = new SqlCommand();
        // Objeto SqlParameter para adicionar os parâmetros necessários em nossas consultas
        public static SqlParameter parametro = new SqlParameter();

        public static IConfigurationRoot Configuration { get; set; }

        public static SqlConnection connection()
        {
            try
            {
                // Obtemos os dados da conexão existentes no appsettings.json utilizando o ConfigurationManager
                GetConexaoAppSettings();

                // Instanciando o objeto SqlConnection
                sqlconnection = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);

                //Verifica se a conexão esta fechada.
                if (sqlconnection.State == ConnectionState.Closed)
                {
                    //Abre a conexão.
                    sqlconnection.Open();
                }
                //Retorna o sqlconnection.              
                return sqlconnection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public void Open()
        {
            sqlconnection.Open();
        }

        public void Close()
        {
            sqlconnection.Close();
        }


        public void AdicionarParametro(string nome, SqlDbType tipo, int tamanho, object valor)
        {
            // Cria a instância do Parâmetro e adiciona os valores
            parametro = new SqlParameter();
            parametro.ParameterName = nome;
            parametro.SqlDbType = tipo;
            parametro.Size = tamanho;
            parametro.Value = valor;
            // Adiciona ao comando SQL o parâmetro
            comando.Parameters.Add(parametro);
        }

        public void AdicionarParametro(string nome, SqlDbType tipo, object valor)
        {
            // Cria a instância do Parâmetro e adiciona os valores
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nome;
            parametro.SqlDbType = tipo;
            parametro.Value = valor;
            // Adiciona ao comando SQL o parâmetro
            comando.Parameters.Add(parametro);
        }


        public void RemoverParametro(string pNome)
        {
            // Verifica se existe o parâmetro
            if (comando.Parameters.Contains(pNome))
                // Se exite remove o mesmo
                comando.Parameters.Remove(pNome);
        }

        public void LimparParametros()
        {
            comando.Parameters.Clear();
        }

        public DataTable ExecutaConsulta(string sql)
        {
            try
            {
                // Pega conexão com a base SQL Server
                comando.Connection = connection();
                // Adiciona a instrução SQL
                comando.CommandText = sql;
                //Executa a query sql.
                comando.ExecuteScalar();
                // Ler os dados e passa para um DataTable
                IDataReader dtreader = comando.ExecuteReader();
                DataTable dtresult = new DataTable();
                dtresult.Load(dtreader);
                // Fecha a conexão
                sqlconnection.Close();
                //Retorna o DataTable com os dados da consulta
                return dtresult;
            }
            catch (Exception ex)
            {
                // Retorna uma exceção simples que pode ser tratada por parte do desenvolvedor
                // Exemplo: if (ex.Message.toString().Contains(‘Networkig’)) 
                // Exemplo throw new Exception(‘Problema de rede detectado’);                        
                throw ex;
            }
        }


        public int ExecutaAtualizacao(string sql)
        {
            int result = 0;

            try
            {
                //Instância o sqlcommand com a query sql que será executada e a conexão.
                //comando = new SqlCommand(sql, connection());
                comando.Connection = connection();
                comando.CommandText = sql;
                //Executa a query sql.
                result = comando.ExecuteNonQuery();
                sqlconnection.Close();
                // Retorna a quantidade de linhas afetadas

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message.ToString());
                // Retorna uma exceção simples que pode ser tratada por parte do desenvolvedor
                //throw ex;
            }
            return result;
        }

        private static void GetConexaoAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

    }
}
