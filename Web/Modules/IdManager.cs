namespace Web.Modules {

    public class IdManager {
        public static string GenerateGUID() {
            return Guid.NewGuid().ToString("N");
        }
    }

}
