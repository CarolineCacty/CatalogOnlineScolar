using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogScolarOnline.Model
{
    public class UserRepository
    {
        public bool ValidateUser(string Email, string Password)
        {
            bool valid;

            using (OnlineSchoolCatalogDataContext context = new OnlineSchoolCatalogDataContext())
            {
                var user = context.Utilizatoris.FirstOrDefault(u => u.Email == Email && u.Parola == Password);
                if (user != null)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
            return valid;
        }
    }
}
