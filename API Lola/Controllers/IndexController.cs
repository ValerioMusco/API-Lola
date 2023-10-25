using Gallery_Lola_DAL.Exceptions;
using Gallery_Lola_DAL.Interfaces;
using Gallery_Lola_DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Lola.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class IndexController : ControllerBase {

        private readonly IIndexService _indexService;

        public IndexController(IIndexService indexService) {

            _indexService = indexService;
        }

        [HttpGet("GenerateToken/")]
        public IActionResult GenerateToken( string token = "" ) {

            throw new NotImplementedException();
        }

        [HttpGet("GetRandomPictures/")]
        public IActionResult GetRandomPicture() {

            foreach(string strings in _indexService.GetRandomPictures()) {
                Console.WriteLine(strings);
            }

            return Ok();
        }

        [HttpGet("CheckQuerryOrUnlock/{userInput}")]
        public IActionResult CheckQuerryOrUnlock(string userInput) {

            int result = _indexService.CheckQuerryOrUnlock(userInput);

            if( result == 0 )
                return Ok( $"?search={userInput}" );
            if(result == 1 ) {

                // Ajout dans Group (UserAccess)
                try {

                    _indexService.AddToGroup( userInput, "test" );
                }
                catch(FailedToAddToGroupException ex) {

                    Console.WriteLine( ex.Message );
                }
                catch(Exception ex) {

                    Console.WriteLine( ex.Message );
                }
            }
            if(result == 2 ) {

                // Ajout dans Folder (UserUnlock)
                try {

                    _indexService.AddFolder( userInput, "test" );
                }
                catch( FailedToAddFolderException ex) {
                    Console.WriteLine( ex.Message );
                }
                catch (Exception ex) {
                    Console.WriteLine( ex.Message );
                }
            }

            // Redirection vers myfolder
            return Ok( "/MyFolder" );
        }
    }
}
