using Microsoft.AspNetCore.Authorization;
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

        public PlaylistController(IPlaylistRepository playlistRepository, IUsuarioRepository usuarioRepository)
        {
            _playlistRepository = playlistRepository;
            _usuarioRepository = usuarioRepository;
        }

		/// <summary>
		/// Retorna todas as playlists cadastradas.
		/// </summary>
		/// <returns>Lista de playlists.</returns>
		[Authorize]
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
		[Authorize]
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
		[Authorize]
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
		[Authorize]
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
				Itens = dto.IdConteudos.Select(conteudo => new ItemPlaylist { ConteudoID = conteudo }).ToList()
			};

            _playlistRepository.UpdatePlaylist(playlist);

            return NoContent();
        }

		/// <summary>
		/// Exclui uma playlist
		/// </summary>
		[Authorize]
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
