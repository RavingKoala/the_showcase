namespace Web.Services
{

    public class IdManager
    {
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }

}
