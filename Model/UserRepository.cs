using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CatalogScolarOnline.Model
{
    public class UserRepository
    {
        public bool ValidateUser(string Email, string Password)
        {
            bool valid;

            using (Online_School_CatalogEntities context = new Online_School_CatalogEntities())
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

        public bool RegisterUser(string email, string password, int rol)
        {
            using (Online_School_CatalogEntities context = new Online_School_CatalogEntities())
            {
                if (context.Utilizatoris.Any(u => u.Email == email))
                {
                    return false; 
                }

                if(!(context.Conturis.Any(u => u.Email == email) && context.Conturis.Any(u => u.Rol == rol)))
                {
                    return false; 
                }

                var newUser = new Utilizatori
                {
                    Email = email,
                    Parola = password,
                    Rol = rol
                };
                context.Utilizatoris.Add(newUser);
                context.SaveChanges();
                return true;
            }
        }
        public void InsertProfesor(Online_School_CatalogEntities context, int userId, string LastName, string FirstName, string GradDidactic)
        {
            var profesor = new Profesori
            {
                UtilizatorID = userId,
                Nume = LastName,
                Prenume = FirstName,
                Grad = GradDidactic
            };

            context.Profesoris.Add(profesor);
            context.SaveChanges();
        }

        public bool InsertElev(Online_School_CatalogEntities context, int userId, string ClasaID, string NumeParinte, string PrenumeParinte,string LastName, string FirstName, string Adresa, DateTime DataNasterii)
        {
            try
            {
                var parinte = context.Parintis.FirstOrDefault(n => n.Nume_parinte == NumeParinte && n.Prenume_parinte == PrenumeParinte);

                if (parinte == null)
                {
                    MessageBox.Show("Părintele specificat nu există în baza de date.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                int ParinteID = parinte.ParinteID;

                var elev = new Elevi
                {
                    UtilizatorID = userId,
                    Nume = LastName,
                    Prenume = FirstName,
                    ClasaID = ClasaID,
                    Data_nasterii = DataNasterii,
                    Adresa = Adresa,
                    ParinteID = ParinteID
                };

                context.Elevis.Add(elev);
                context.SaveChanges();
                MessageBox.Show("Elevul a fost adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("FK_Clase"))
                {
                    MessageBox.Show("Clasa specificată nu există. Verificați ClasaID.", "Eroare Cheie Străină", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (ex.Message.Contains("FK_Parinti"))
                {
                    MessageBox.Show("Părintele specificat nu există. Verificați datele părintelui.", "Eroare Cheie Străină", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (ex.Message.Contains("FK_Utilizatori"))
                {
                    MessageBox.Show("Utilizatorul specificat nu există. Verificați UtilizatorID.", "Eroare Cheie Străină", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    MessageBox.Show($"A apărut o eroare la inserare: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare neașteptată: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool InsertParinte(Online_School_CatalogEntities context, int userId, string LastName, string FirstName, string Telefon)
        {
            try
            {
                var parinte = new Parinti
                {
                    UtilizatorID = userId,
                    Nume_parinte = LastName,
                    Prenume_parinte = FirstName,
                    Numar_telefon = Telefon
                };
                context.Parintis.Add(parinte);
                context.SaveChanges();
                return true;
            }

            catch (SqlException ex)
            {
                if (ex.Message.Contains("FK_Utilizatori"))
                {
                    MessageBox.Show("Utilizatorul specificat nu există. Verificați UtilizatorID.", "Eroare Cheie Străină", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    MessageBox.Show($"Eroare neașteptată: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            
        }

    }
}
