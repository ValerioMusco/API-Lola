using Gallery_Lola_DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Lola.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class AdminController : ControllerBase {

        private readonly IAdminService _adminService;

        public AdminController( IAdminService adminService ) {

            _adminService = adminService;
        }

        [HttpDelete("/DeleteFolder")]
        public IActionResult DeleteFolder( int folderId ) {

            Directory.Delete( _adminService.PathToDirectory( folderId ), true );
            _adminService.Delete( folderId, "Pictures" );
            _adminService.Delete( folderId, "Miniatures" );
            _adminService.DeleteFolder( folderId );
            return Ok();
        }

        [HttpDelete("/DeleteFiles")]
        public IActionResult UpdateFolderContent( int folderId, [FromForm] IEnumerable<string> pictures, [FromForm] IEnumerable<string> miniatures ) {

            string dirPath = _adminService.PathToDirectory( folderId );
            foreach( string p in pictures )
                System.IO.File.Delete( Path.Combine( dirPath, p ) );
            foreach( string m in miniatures )
                System.IO.File.Delete( Path.Combine( dirPath, m ) );

            _adminService.DeleteFiles( folderId, pictures, miniatures );
            return Ok();
        }

        [HttpPatch("/ChangeFolderName")]
        public IActionResult UpdateFolderName( int folderId, string newName ) {

            string newPath = "E:\\FTP\\" + newName;
            string dirPath = _adminService.PathToDirectory( folderId );
            try {
                Directory.CreateDirectory( dirPath );
                Directory.Move( dirPath, newPath );
                Directory.Delete( newPath );
            }
            catch( IOException ioe ) {
                Console.WriteLine( ioe.ToString() );
            }
            catch( Exception e ) {
                Console.WriteLine( e.ToString() );
            }
            _adminService.UpdateFolder( folderId, newName );
            return Ok();
        }

        [HttpPost("/Upload")]
        public async Task<IActionResult> UploadNewFolder( [FromForm] string folderName, [FromForm] List<IFormFile> files ) {

            string ftp = "E:\\FTP";
            string newDir = Path.Combine( ftp, folderName );
            string[] miniaturesAndExtensions = { "_m.jpg", "_m.png", "_m.jpeg", "_m.txt" };
            List<string> pictures = new();
            List<string> miniatures = new();

            Directory.CreateDirectory( newDir );

            if( files is null || files.Count == 0 )
                return BadRequest( "Aucun fichier n'a été fourni" );

            try {

                foreach( var file in files ) {

                    if( file.Length == 0 )
                        continue;

                    string filePath = Path.Combine( newDir, file.FileName );

                    using( var stream = new FileStream( filePath, FileMode.Create ) )
                        await file.CopyToAsync( stream );
                }

                miniatures = files.Select(f => f.FileName).Where( f => miniaturesAndExtensions.Any( ext => f.EndsWith(ext, StringComparison.OrdinalIgnoreCase) ) ).ToList();
                pictures = files.Select( f => f.FileName ).Except( miniatures ).ToList();
                _adminService.AddFolder( folderName, pictures, miniatures );

                return Ok( "Dossier ajouté avec succès" );
            }
            catch( Exception ex ) {
                return StatusCode( 500, $"Une erreur s'est produite : {ex.Message}" );
            }
        }
    }
}
