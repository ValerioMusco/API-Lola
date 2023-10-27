namespace Gallery_Lola_DAL.Exceptions {
    public class CheckAccessException : Exception {

        public CheckAccessException(){}

        public CheckAccessException(string message) : base( message ) { }
    }
}
