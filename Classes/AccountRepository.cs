using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    public class AccountRepository : iRepository<Account>
    {
        List<Account> Listing = new List<Account>();
        public Account Get(int IdItem)
        {
            if(IsInBounds(IdItem))
                return Listing[IdItem];

            return null;
        }

        public List<Account> GetList()
        {
            return Listing;
        }

        public int GetNextId()
        {
            return Listing.Count;
        }

        public void Insert(Account Entity)
        {
            Listing.Add(Entity);
        }

        public void Remove(int IdItem)
        {
            if(IsInBounds(IdItem))
                Listing[IdItem].Delete();
        }

        public void Update(int IdItem, Account Entity)
        {
            if(IsInBounds(IdItem))
                Listing[IdItem] = Entity;
        }
        public override string ToString()
        {
            string output  = "";
            int counter=1;
            
            foreach(Account s in Listing)
            {
                output+=counter+" | "+s.ToString()+Environment.NewLine;
                counter++;
            }

            return output;
        }
        private bool IsInBounds(int IdItem)
        {
            return (Listing.Count > IdItem && IdItem >= 0);
        }
        
    }
}