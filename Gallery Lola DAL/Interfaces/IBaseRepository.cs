namespace Gallery_Lola_DAL.Interfaces {
    public interface IBaseRepository {

        IEnumerable<string> GetFolderContent( int folderId, bool miniatures = false);
    }
}
