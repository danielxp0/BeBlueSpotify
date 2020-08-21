using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeBlueSpotify.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BeBlueSpotify.Arquitetura.Paginacao;
using BeBlueSpotify.Arquitetura.Modelo;

namespace BeBlueSpotify.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly BeBlueSpotifyContext _context;

        public VendasController(BeBlueSpotifyContext context)
        {
            _context = context;
        }

        // GET: api/Vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVenda()
        {
            return await _context.Venda.ToListAsync();
        }

   

        // PUT: api/Vendas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(int id, Venda venda)
        {
            if (id != venda.Idvenda)
            {
                return BadRequest();
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

     

        // DELETE: api/Vendas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Venda>> DeleteVenda(int id)
        {
            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Venda.Remove(venda);
            await _context.SaveChangesAsync();

            return venda;
        }

        private bool VendaExists(int id)
        {
            return _context.Venda.Any(e => e.Idvenda == id);
        }


        // GET: api/Vendas
        /// <summary>
        /// Consultar todas as vendas efetuadas de forma paginada, filtrando pelo range
        /// de datas(inicial e final) da venda e ordenando de forma decrescente pela
        /// data da venda;
        /// </summary>
        [HttpGet]
        public List<Venda> VendaPeriodo(DateTime pDataInicial, DateTime pDataFinal, int paginaAtual = 1)
        {
            if (pDataFinal == null || pDataFinal == null)
                return null;

            return _context.Venda.Where(r => r.Data >= pDataInicial.Date && r.Data < pDataFinal.Date.AddDays(1))
                .OrderByDescending(r => r.Data).GetPaged(paginaAtual, 20).Results.ToList();
        }


        // GET: api/Vendas/5
        /// <summary>
        /// Consultar uma venda pelo seu identificador;
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVenda(int id)
        {
            var venda = await _context.Venda.FindAsync(id);

            if (venda == null)
            {
                return NotFound();
            }

            return venda;
        }


        // POST: api/Vendas
        /// <summary>
        /// Registrar uma nova venda de discos calculando o valor total de cashback
        /// considerando a tabela.
        /// </summary>                
        [HttpPost]
        public async Task<ActionResult<Venda>> PostVenda(int idUsuario, List<MVendaItem> lstItens)
        {
            Venda venda = new Venda();

            //Consultando o usuário da Venda
            var usuario = _context.Usuario.FirstOrDefault(r => r.Idusuario == venda.Idusuario) ;
            if (usuario == null)
                throw new Exception("Usuário não encontrado!");

            venda.Idusuario = idUsuario;
            venda.Data = DateTime.Now;
            

            List<VendaItem> lstVendaItem = new List<VendaItem>();

            foreach (var item in lstItens)
            {
                VendaItem vi = new VendaItem();
                vi.Idalbum = item.IDAlbum;
                vi.Qtd = item.Qtd;
                
                //Consulta o album no banco de dados!
                var album = _context.Album.FirstOrDefault(r => r.Idalbum == item.IDAlbum);
                if (album != null)
                {                    
                    //Verifica se naquele dia da semana existe algum cash para o genero do album
                    var cashDay = _context.CashSemanal.FirstOrDefault(r => r.DiaSemana == (int)DateTime.Now.DayOfWeek && r.Idgenero == album.Idgenero);
                    if (cashDay != null)
                    {
                        //Calcula a porcentagem de retorno do Cash
                        vi.Cash = (item.Qtd * album.Valor) * cashDay.Cash / 100;
                    }
                }
            }

            //Persiste a Venda
            _context.Venda.Add(venda);
            _context.SaveChanges();


            //Persiste os itens da Venda
            foreach (var item in lstVendaItem)
            {
                item.Idvenda = venda.Idvenda;
                _context.VendaItem.Add(item);
            }

            //Guarda o Cash Acumulado 
            usuario.CashAcumulado += lstVendaItem.Sum(r => r.Cash);
            _context.Entry(usuario);
            await _context.SaveChangesAsync();




            return CreatedAtAction("GetVenda", new { id = venda.Idvenda }, venda);
        }

    }
}
