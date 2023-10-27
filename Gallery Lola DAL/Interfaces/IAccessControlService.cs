namespace Gallery_Lola_DAL.Interfaces {
    public interface IAccessControlService {

        bool CheckAccess( int folderId, string token );
    }
}
