using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BeBlueSpotify.Arquitetura.Util;
using BeBlueSpotify.Model;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

namespace BeBlueSpotify.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SpotifyController : Controller
    {
        [HttpGet]
        public void CargaInicial()
        {
            try
            {
                var context = new BeBlueSpotifyContext();
                var clienteID = Configuracao.GetConfiguracao().GetSection("Config:SpotifyClienteID").Value;
                var clienteSecret = Configuracao.GetConfiguracao().GetSection("Config:SpotifyClienteSecret").Value;

                var config = SpotifyClientConfig
                            .CreateDefault()
                                .WithAuthenticator(new ClientCredentialsAuthenticator(clienteID, clienteSecret));

                var spotify = new SpotifyClient(config);
                var generos = context.Genero.ToList();
                Random rng = new Random();
                int[] centavos = new int[2];
                centavos[0] = 0;
                centavos[1] = 50;
                CultureInfo cultura = new CultureInfo("pt-Br");
                foreach (var item in generos)
                {
                    var searchRequest = new SearchRequest(SearchRequest.Types.Album, item.Nome);
                    searchRequest.Limit = 50;
                    var data = spotify.Search.Item(searchRequest).Result.Albums.Items;

                    foreach (var itemAlbum in data)
                    {
                        var preco = Convert.ToDecimal(rng.Next(10, 100) + "," + centavos[rng.Next(2)], cultura);

                        context.Album.Add(new Album()
                        {
                            Artista = itemAlbum.Artists.FirstOrDefault()?.Name,
                            Idgenero = item.Idgenero,
                            Nome = itemAlbum.Name,
                            Valor = preco
                        });
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
