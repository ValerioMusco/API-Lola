using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Lola.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class GalleryController : ControllerBase {

        [HttpGet("GetYears/")]
        public IActionResult GetYears() {

            throw new NotImplementedException();
        }

        [HttpGet("Search/{querry}")]
        public IActionResult Search( string querry ) {

            throw new NotImplementedException();
        }

        [HttpGet( "GetFolderContent/{folderId}" )]
        public IActionResult GetFolderContent( int folderId ) {

            throw new NotImplementedException();
        }

        [HttpGet( "AddFavorite/{folderId}" )]
        public IActionResult AddFavorite( int folderId, string userToken ) {

            throw new NotImplementedException();
        }
    }
}
