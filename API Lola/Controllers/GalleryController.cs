using Gallery_Lola_DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API_Lola.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class GalleryController : ControllerBase {

        private readonly IGalleryService _galleryService;
        private readonly IAccessControlService _accessControlService;

        public GalleryController(IGalleryService galleryService, IAccessControlService accessControlService){
            
            _galleryService = galleryService;
            _accessControlService = accessControlService;
        }

        [HttpGet("GetYears/")]
        public IActionResult GetYears() {

            return Ok(_galleryService.GetYears());
        }

        [HttpGet("Search/{querry}")]
        public IActionResult Search( string querry ) {

            return Ok(_galleryService.Search(querry));
        }

        [HttpGet( "GetFolderContent/{folderId}" )]
        public IActionResult GetFolderContent( int folderId, string token ) {

            if( !_accessControlService.CheckAccess( folderId, token ) )
                return BadRequest( "Vous n'avez pas accès à ce dossier" );
            return Ok(
                new {

                    FullSize = _galleryService.GetFolderContent( folderId ),
                    Miniatures = _galleryService.GetFolderContent( folderId, true )
                }
            );
        }

        [HttpGet( "AddFavorite/{folderId}" )]
        public IActionResult AddFavorite( int folderId, string userToken ) {

            if( !_accessControlService.CheckAccess( folderId, userToken ) )
                return BadRequest( "Vous ne pouvez pas ajouter ce dossier au favoris." );
            try {

                bool flag = _galleryService.AddToFavorite(folderId, userToken);

                if( flag )
                    return Ok( "Le dossier à été ajouté au favoris." );
                return BadRequest( "Erreur lors de l'ajout du dossier dans les favoris." );
            }
            catch( Exception ex ) {

                return BadRequest( "Erreur fatale lors de l'ajout. " + ex.Message );
            }
        }

        [HttpGet("GetAll/")]
        public IActionResult GetAll() {

            return Ok( _galleryService.GetAll());
        }
    }
}
