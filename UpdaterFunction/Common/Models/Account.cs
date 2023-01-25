using System;

namespace UpdaterCloudFunction.Common.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public int Currency { get; set; }
        public int BankBik { get; set; }
        public string AccountType { get; set; }
        public DateTime ActivationDate { get; set; }
        public Balance Balance { get; set; }
    }
}

