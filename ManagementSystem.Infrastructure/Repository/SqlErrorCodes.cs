namespace ManagementSystem.Infrastructure.Repositories
{
    public static class SqlErrorCodes
    {
        public const int UniqueConstraintViolation = 2627;
        public const int CheckConstraintViolation = 547;
        public const int ForeignKeyViolation = 547;
    }
}