using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePassword.Core.Interfaces
{
    public interface IPasswordService
    {
        string GeneratePassword(string userId);
        string GeneratePassword(string userId, long iterationNumber, int digits);
        bool IsPasswordValid(string userId, string password);
        bool IsPasswordValid(string userId, string password, long iterationNumber, int digits);
    }
}
