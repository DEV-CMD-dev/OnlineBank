namespace OnlineBank.Data.Services
{
    public static class ServiceInitializer
    {
        public static void InitAll(BankDbContext db)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));

            UserService.Init(db);
            CardService.Init(db);
        }
    }

}
