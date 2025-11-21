using Microsoft.AspNetCore.Mvc;
using PIMVIII.DTOs;
using PIMVIII.Models;
using PIMVIII.Repositories;

namespace PIMVIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        IPlaylistRepository _playlistRepository;
        IUsuarioRepository _usuarioRepository;
        IConteudoRepository _conteudoRepository;

        public PlaylistController(IPlaylistRepository playlistRepository, IUsuarioRepository usuarioRepository, IConteudoRepository conteudoRepository)
        {
            _playlistRepository = playlistRepository;
            _usuarioRepository = usuarioRepository;
            _conteudoRepository = conteudoRepository;
        }

        /// <summary>
        /// Retorna todas as playlists cadastradas.
        /// </summary>
        /// <returns>Lista de playlists.</returns>
        [HttpGet]
        public ActionResult<List<PlaylistDto>> GetAll()
        {
            var playlists = _playlistRepository.GetAllPlaylists();

            return Ok(playlists.Select(PlaylistDto.MapFrom).ToList() ?? new List<PlaylistDto>()); 
        }

        /// <summary>
        /// Retorna uma playlist localizada pelo Id informado.
        /// </summary>
        /// <returns>Playlist</returns>
        [HttpGet("{id}")]
        public ActionResult<PlaylistDto> GetById(int id)
        {
            var playlist = _playlistRepository.GetPlaylistByID(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(PlaylistDto.MapFrom(playlist));
        }

        /// <summary>
        /// Adiciona uma nova playlist.
        /// </summary>
        /// <returns>Playlist.</returns>
        [HttpPost]
        public ActionResult Add(PlaylistAddDto dto)
        {
            var playlist = new Playlist
            {
                Nome = dto.Nome,
                UsuarioID = dto.IdUsuario,
                Itens = dto.IdConteudos.Select(conteudo => new ItemPlaylist { ConteudoID = conteudo }).ToList()
            };

            _playlistRepository.AddPlaylist(playlist);

            return CreatedAtAction(nameof(GetById), new { id = playlist.ID }, new { id = playlist.ID });
        }

        /// <summary>
        /// Atualiza uma playlist
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult Update(int id, PlaylistUpdateDto dto)
        {
            var existingPlaylist = _playlistRepository.GetPlaylistByID(id);

            if (existingPlaylist == null)
            {
                return NotFound(new { Message = "Playlist não encontrada." });
            }

            var playlist = new Playlist
            {
                ID = id,
                Nome = dto.Nome,
                Conteudos = _conteudoRepository.GetConteudosByIds(dto.IdConteudos)
            };

            _playlistRepository.UpdatePlaylist(playlist);

            return NoContent();
        }

        /// <summary>
        /// Exclui uma playlist
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var playlist = _playlistRepository.GetPlaylistByID(id);

            if (playlist == null)
            {
                return NotFound(new { Message = "Playlist não encontrada." });
            }

            _playlistRepository.DeletePlaylist(id);

            return NoContent();
        }
    }
}
