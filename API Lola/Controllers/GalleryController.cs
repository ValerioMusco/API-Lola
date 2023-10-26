using Gallery_Lola_DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Lola.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class GalleryController : ControllerBase {

        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService){
            
            _galleryService = galleryService;
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
        public IActionResult GetFolderContent( int folderId ) {

            return Ok(
                new {

                    FullSize = _galleryService.GetFolderContent( folderId ),
                    Miniatures = _galleryService.GetFolderContent( folderId, true )
                }
            );
        }

        [HttpGet( "AddFavorite/{folderId}" )]
        public IActionResult AddFavorite( int folderId, string userToken ) {

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
    }
}
