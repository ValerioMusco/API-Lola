using API_Lola.Tools;
using Gallery_Lola_DAL.Exceptions;
using Gallery_Lola_DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Lola.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class IndexController : ControllerBase {

        private readonly IIndexService _indexService;
        private readonly TokenManager _tokenManager;

        public IndexController(IIndexService indexService, TokenManager token) {

            _indexService = indexService;
            _tokenManager = token;

        }

        [HttpGet("GenerateToken/")]
        public IActionResult GenerateToken( ) {

            string json;
            json = JsonConvert.SerializeObject(
                new {
                    token = _tokenManager.GenerateToken()
                }
            );
            return Ok( json );
        }

        [HttpGet("GetRandomPictures/")]
        public IActionResult GetRandomPicture() {

            List<string> pictures = new();
            foreach(string strings in _indexService.GetRandomPictures())
                pictures.Add(strings);

            return Ok(pictures);
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
