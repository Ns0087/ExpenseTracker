namespace Expense_Tracker.Services.Interfaces
{
    public interface ICryptographicService
    {
        public string Encrypt(string userId);
        public int Decrypt(string cipherText);
    }
}
