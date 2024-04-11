using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PayAndPlay.Data;
using System.Globalization;
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
                .Where(p => p.DJId == DjId && p.Estado == "CONCLUIDO")
                .Sum(p => p.Custo_Pedido);
            return Saldo;
        }
        public decimal ListarGanhosMes(int? data, int DJ_Id)
        {
            // Listar os ganhos de um DJ num mês
            decimal GanhosMes = _context.Tpedidos.Where(p => p.DJId == DJ_Id && p.Data.Month == data && p.Estado == "CONCLUIDO").Sum(p => p.Custo_Pedido);

            return GanhosMes;
        }
        public Dictionary<string, decimal> ListarGanhosPeriodo(int? dataInicio, int? dataFim, int DJ_Id)
        {
            // Listar os ganhos de um DJ por período
            var GanhosPeriodo = _context.Tpedidos
                .Where(p => p.DJId == DJ_Id && p.Data.Month >= dataInicio && p.Data.Month <= dataFim && p.Estado == "CONCLUIDO")
                .GroupBy(p => p.Data.Month)
                .Select(g => new
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    TotalWinnings = g.Sum(p => p.Custo_Pedido)
                })
                .ToDictionary(x => x.Month, x => x.TotalWinnings);

            return GanhosPeriodo;
        }

        public string ListarMusicasMaisPedidas(int DJId)
        {
            string NomeMusicasMaisPedidas = "";
            // Listar as músicas mais pedidas de um DJ 
            Musica MusicasMaisPedidas = _context.Tpedidos
                .Where(p => p.DJId == DJId && p.Estado == "CONCLUIDO")
                .GroupBy(m => m.MusicaInPlayList.Musica.Artista)
                .OrderBy(g => g.Count())
                .Select(m => m.FirstOrDefault().MusicaInPlayList.Musica)
                .FirstOrDefault();
            if (MusicasMaisPedidas != null)
            {
                NomeMusicasMaisPedidas = MusicasMaisPedidas.Nome.ToString();
            }
            else
            {
                NomeMusicasMaisPedidas = "Não existem músicas pedidas";
            }

            return NomeMusicasMaisPedidas;
        }
        public string ListarMusicasMenosPedidas(int DJId)
        {
            string NomeMusicasMenosPedidas = "";
            // Listar as músicas menos pedidas de um DJ 
            Musica MusicasMenosPedidas = _context.Tpedidos
                .Where(p => p.DJId == DJId && p.Estado == "CONCLUIDO")
                .GroupBy(m => m.MusicaInPlayList.Musica.Artista)
                .OrderBy(g => g.Count())
                .Select(m => m.FirstOrDefault().MusicaInPlayList.Musica)
                .LastOrDefault();
            if (MusicasMenosPedidas != null)
            {
                NomeMusicasMenosPedidas = MusicasMenosPedidas.Nome.ToString();
            }
            else
            {
                NomeMusicasMenosPedidas = "Não existem músicas pedidas";
            }

            return NomeMusicasMenosPedidas;
        }
        public string ListarUtilizadorMaisPedidos(int DjId)
        {
            string NomeUtilizadorMaisPedidos = "";
            // Listar o utilizador que mais pedidos fez
            Utilizador UtilizadorMaisPedidos = _context.Tpedidos
                .Where(p => p.DJId == DjId && p.Estado == "CONCLUIDO")
                .GroupBy(u => u.UtilizadorId)
                .OrderBy(g => g.Count())
                .Select(u => u.FirstOrDefault().Utilizador)
                .FirstOrDefault();
            if (UtilizadorMaisPedidos != null)
            {
                NomeUtilizadorMaisPedidos = UtilizadorMaisPedidos.UserName.ToString();
            }
            else
            {
                NomeUtilizadorMaisPedidos = "Não existem Pedidos";
            }
            return NomeUtilizadorMaisPedidos;
        }
        public string ListarUtilizadorMenosPedidos(int DjId)
        {
            string NomeUtilizadorMenosPedidos = "";
            // Listar o utilizador que menos pedidos fez
            Utilizador UtilizadorMenosPedidos = _context.Tpedidos
                .Where(p => p.DJId == DjId && p.Estado == "CONCLUIDO")
                .GroupBy(u => u.UtilizadorId)
                .OrderBy(g => g.Count())
                .Select(u => u.FirstOrDefault().Utilizador)
                .LastOrDefault();
            if (UtilizadorMenosPedidos != null)
            {
                NomeUtilizadorMenosPedidos = UtilizadorMenosPedidos.UserName.ToString();
            }
            else
            {
                NomeUtilizadorMenosPedidos = "Não existem Pedidos";
            }
            return NomeUtilizadorMenosPedidos;
        }
        public string ListarUtilizadorMaisGastos(int DjId)
        {
            string NomeUtilizadorMaisGastos = "";
            // Listar o utilizador que mais gastou
            Utilizador UtilizadorMaisGastos = _context.Tpedidos
                .Where(p => p.DJId == DjId && p.Estado == "CONCLUIDO")
                .GroupBy(u => u.UtilizadorId)
                .OrderBy(g => g.Sum(p => p.Custo_Pedido))
                .Select(u => u.FirstOrDefault().Utilizador)
                .FirstOrDefault();
            if (UtilizadorMaisGastos != null)
            {
                NomeUtilizadorMaisGastos = UtilizadorMaisGastos.UserName.ToString();
            }
            else
            {
                NomeUtilizadorMaisGastos = "Não existem Pedidos";
            }
            return NomeUtilizadorMaisGastos;
        }
        public string ListarUtilizadorMenosGastos(int DjId)
        {
            string NomeUtilizadorMenosGastos = "";
            // Listar o utilizador que menos gastou
            Utilizador UtilizadorMenosGastos = _context.Tpedidos
                .Where(p => p.DJId == DjId && p.Estado == "CONCLUIDO")
                .GroupBy(u => u.UtilizadorId)
                .OrderByDescending(g => g.Sum(p => p.Custo_Pedido))
                .Select(u => u.FirstOrDefault().Utilizador)
                .FirstOrDefault();
            if (UtilizadorMenosGastos != null)
            {
                NomeUtilizadorMenosGastos = UtilizadorMenosGastos.UserName.ToString();
            }
            else
            {
                NomeUtilizadorMenosGastos = "Não existem Pedidos";
            }
            return NomeUtilizadorMenosGastos;
        }

        // LISTAGENS UTILIZADORES

        public Dictionary<string, decimal> ListarGastosMesPorDj(int? data, int UtilizadorId)
        {
            // Listar os gastos de um utilizador num mês por DJ
            var GastosMes = _context.Tpedidos
                .Where(p => p.UtilizadorId == UtilizadorId && (data == null || p.Data.Month == data) && p.Estado == "CONCLUIDO")
                .GroupBy(p => p.DJId)
                .Select(g => new
                {
                    DJName = g.First().DJ.UserName,
                    TotalSpent = g.Sum(p => p.Custo_Pedido)
                })
                .ToDictionary(x => x.DJName, x => x.TotalSpent);

            return GastosMes;
        }

        public Dictionary<string, decimal> ListarGastosPeriodo(int? dataInicio, int? dataFim, int UtilizadorId)
        {
            // Listar os gastos de um utilizador num período por DJ
            var GastosPeriodo = _context.Tpedidos
                .Where(p => p.UtilizadorId == UtilizadorId && (p.Data.Month >= dataInicio && p.Data.Month <= dataFim) && p.Estado == "CONCLUIDO")
                .GroupBy(p => p.DJId)
                .Select(g => new
                {
                    DJName = g.First().DJ.UserName,
                    TotalSpent = g.Sum(p => p.Custo_Pedido)
                })
                .ToDictionary(x => x.DJName, x => x.TotalSpent);

            return GastosPeriodo;
        }


    }
}
