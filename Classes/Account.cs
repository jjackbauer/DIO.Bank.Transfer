namespace DIO.Bank
{
    public class Account : BaseEntity
    {
        public AccountKind accountKind { get; private set; }
        public double balance { get; private set; }
        public double credit { get; private set; }
        public string clientName { get; private set; }
        public bool deleted { get; private set; }

        public Account(AccountKind accountKind, double balance, double credit, string clientName, bool deleted)
        {
            this.accountKind = accountKind;
            this.balance = balance;
            this.credit = credit;
            this.clientName = clientName;
            this.deleted = deleted;

        }
        public void Delete()
        {
            this.deleted = true;
        }

        public bool Withdraw(double value)
        {
            if(this.balance - value < (this.credit*-1))
                return false;
            
            this.balance-=value;
            return true;
        }
        public bool Deposit(double value)
        {   
            if(value <= 0)
                return false;
            
            this.balance+=value;
            return true;
        }
        public bool Transfer(double value, Account destiny)
        {
            if(destiny == null || this.Withdraw(value) == false)
                return false;

            destiny.Deposit(value);
            return true;
        }

        public override string ToString()
        {
            string @return = "";
            @return += "Account Kind: " + this.accountKind + " | ";
            @return += "Client Name: " + this.clientName + " | ";
            @return += "Balance:  " + this.balance + " | ";
            @return += "Credit: " + this.credit;
            return @return;
        }
    }
}