using Gallery_Lola_DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Lola.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class FolderController : ControllerBase {

        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService) {
            _folderService = folderService;
        }

        [HttpDelete("SupprimerDossier/")]
        public IActionResult RemoveUnlock( int folderId, string userToken ) {

            try {

                _folderService.Remove( folderId, userToken, "UserUnlock" );
                return Ok("Élément supprimé avec succès");
            }
            catch( Exception ex ) {

                return BadRequest( "Échec lors de la suppression du dossier. : " +  ex.Message );
            }
        }

        [HttpDelete( "SupprimerFavoris/")]
        public IActionResult RemoveFav( int folderId, string userToken ) {

            try {

                _folderService.Remove( folderId, userToken, "UserFav" );
                return Ok( "Élément supprimé avec succès" );
            }
            catch( Exception ex ) {

                return BadRequest( "Échec lors de la suppression du favoris. : " + ex.Message );
            }
        }

        [HttpGet( "Dossier/{folderId}" )]
        public IActionResult GetFolderContent( int folderId ) {

            return Ok(
                new {
                    FullSize = _folderService.GetFolderContent( folderId ),
                    Miniature = _folderService.GetFolderContent( folderId, true )
                }
            );
        }
    }
}
