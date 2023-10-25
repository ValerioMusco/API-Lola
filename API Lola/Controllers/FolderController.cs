using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Lola.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class FolderController : ControllerBase {

        [HttpPost("SupprimerDossier/")]
        public IActionResult RemoveUnlock( string folderId, string userToken ) {

            throw new NotImplementedException();
        }

        [HttpPost("SupprimerFavoris/")]
        public IActionResult RemoveFav( string folderId, string userToken ) {

            throw new NotImplementedException();
        }

        [HttpGet( "Dossier/{folderId}" )]
        public IActionResult GetFolderContent( string folderId ) {

            throw new NotImplementedException();
        }
    }
}
