using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class User
    {
        public decimal Balance { get; private set; }

        public User(decimal initialBalance)
        {
            Balance = initialBalance;
        }

        public bool HasEnoughBalance(decimal amount)
        {
            return Balance >= amount;
        }

        public void UpdateBalance(decimal amount)
        {
            Balance -= amount;
        }

    }
    
}
