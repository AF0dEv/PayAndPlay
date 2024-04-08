﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PayAndPlay.Data;
using System.Linq;

namespace PayAndPlay.Models
{
    public class Listagem
    {
        private readonly ApplicationDbContext _context;

        public Listagem(ApplicationDbContext context)
        {
            _context = context;
        }
        // LISTAGENS DJS
        public decimal CalculoSaldoDJ(int DjId)
        {
            // Calcular o saldo de um DJ
            decimal Saldo = _context.Tpedidos
                .Where(p => p.DJId == DjId && p.Estado == "PAGO")
                .Sum(p => p.Custo_Pedido);
            return Saldo;
        }
        public decimal ListarGanhosMes(int? data, int DJ_Id)
        {
            // Listar os ganhos de um DJ num mês
            decimal GanhosMes = _context.Tpedidos.Where(p => p.DJId == DJ_Id && p.Data.Month == data).Sum(p => p.Custo_Pedido);

            return GanhosMes;
        }
        public decimal ListarGanhosPeriodo(int? dataInicio, int? dataFim, int DJ_Id)
        {
            // Listar os ganhos de um DJ num período
            decimal GanhosPeriodo = _context.Tpedidos.Where(p => p.DJId == DJ_Id && p.Data.Month >= dataInicio && p.Data.Month <= dataFim).Sum(p => p.Custo_Pedido);

            return GanhosPeriodo;
        }
        // test this
        public string ListarMusicasMaisPedidas(int DJId)
        {
            // Listar as músicas mais pedidas de um DJ 
            Musica MusicasMaisPedidas = _context.Tpedidos
                .Where(p => p.DJId == DJId)
                .GroupBy(m => m.MusicaInPlayList.Musica.Artista)
                .OrderBy(g => g.Count())
                .Select(m => m.FirstOrDefault().MusicaInPlayList.Musica)
                .FirstOrDefault();
           string NomeMusicasMaisPedidas = MusicasMaisPedidas.Nome.ToString();

            return NomeMusicasMaisPedidas;
        }
        public string ListarMusicasMenosPedidas(int DJId)
        {
            // Listar as músicas menos pedidas de um DJ 
            Musica MusicasMenosPedidas = _context.Tpedidos
                .Where(p => p.DJId == DJId)
                .GroupBy(m => m.MusicaInPlayList.Musica.Artista)
                .OrderBy(g => g.Count())
                .Select(m => m.FirstOrDefault().MusicaInPlayList.Musica)
                .LastOrDefault();
            string NomeMusicasMenosPedidas = MusicasMenosPedidas.Nome.ToString();

            return NomeMusicasMenosPedidas;
        }
        public string ListarUtilizadorMaisPedidos(int DjId)
        {
            // Listar o utilizador que mais pedidos fez
            Utilizador UtilizadorMaisPedidos = _context.Tpedidos
                .Where(p => p.DJId == DjId)
                .GroupBy(u => u.UtilizadorId)
                .OrderBy(g => g.Count())
                .Select(u => u.FirstOrDefault().Utilizador)
                .FirstOrDefault();
            string NomeUtilizadorMaisPedidos = UtilizadorMaisPedidos.UserName.ToString();
            return NomeUtilizadorMaisPedidos;
        }
        public string ListarUtilizadorMenosPedidos(int DjId)
        {
            // Listar o utilizador que menos pedidos fez
            Utilizador UtilizadorMenosPedidos = _context.Tpedidos
                .Where(p => p.DJId == DjId)
                .GroupBy(u => u.UtilizadorId)
                .OrderBy(g => g.Count())
                .Select(u => u.FirstOrDefault().Utilizador)
                .LastOrDefault();
            string NomeUtilizadorMenosPedidos = UtilizadorMenosPedidos.UserName.ToString();

            return NomeUtilizadorMenosPedidos;
        }
        public string ListarUtilizadorMaisGastos(int DjId)
        {
            // Listar o utilizador que mais gastou
            Utilizador UtilizadorMaisGastos = _context.Tpedidos
                .Where(p => p.DJId == DjId)
                .GroupBy(u => u.UtilizadorId)
                .OrderBy(g => g.Sum(p => p.Custo_Pedido))
                .Select(u => u.FirstOrDefault().Utilizador)
                .FirstOrDefault();
            string NomeUtilizadorMaisGastos = UtilizadorMaisGastos.UserName.ToString();
            return NomeUtilizadorMaisGastos;
        }
        public string ListarUtilizadorMenosGastos(int DjId)
        {
            // Listar o utilizador que menos gastou
            Utilizador UtilizadorMenosGastos = _context.Tpedidos
                .Where(p => p.DJId == DjId)
                .GroupBy(u => u.UtilizadorId)
                .OrderByDescending(g => g.Sum(p => p.Custo_Pedido))
                .Select(u => u.FirstOrDefault().Utilizador)
                .FirstOrDefault();

            string NomeUtilizadorMenosGastos = UtilizadorMenosGastos.UserName.ToString();
            return NomeUtilizadorMenosGastos;
        }

        // LISTAGENS UTILIZADORES

        public decimal ListarGastosMes(DateOnly data, int UtilizadorId)
        {
            // Listar os gastos de um utilizador num mês
            decimal GastosMes = _context.Tpedidos
                .Where(p => p.UtilizadorId == UtilizadorId && p.Data.Month == data.Month && p.Data.Year == data.Year)
                .Sum(p => p.Custo_Pedido);

            return GastosMes;
        }
        public decimal ListarGastosPeriodo(DateOnly dataInicio, DateOnly dataFim, int UtilizadorId)
        {
            // Listar os gastos de um utilizador num período
            decimal GastosPeriodo = _context.Tpedidos
                .Where(p => p.UtilizadorId == UtilizadorId && p.Data >= dataInicio && p.Data <= dataFim)
                .Sum(p => p.Custo_Pedido);

            return GastosPeriodo;
        }
        public decimal ListarGastosPorDj(int UtilizadorId, int DjId)
        {
            // Listar os gastos de um utilizador por DJ
            decimal GastosDj = _context.Tpedidos
                .Where(p => p.UtilizadorId == UtilizadorId && p.DJId == DjId)
                .Sum(p => p.Custo_Pedido);
            return GastosDj;
        }
    }
}