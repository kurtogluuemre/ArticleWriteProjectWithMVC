using System.ComponentModel.DataAnnotations;

namespace MVC_CASE.MyValidations
{
    /// <summary>
    /// Şifre içinde en az bir özel karakter ve en az bir rakam olmasını zorunlu kılar.
    /// </summary>
    public class SifreGuvenlikAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true; // Required zaten ayrı kontrol edilir.

            string stringValue = value.ToString();

            bool hasDigit = false;
            bool hasSpecialChar = false;

            foreach (char c in stringValue)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (!char.IsLetter(c)) // Harf değilse ve rakam da değilse özel karakterdir
                {
                    hasSpecialChar = true;
                }

                
                if (hasDigit || hasSpecialChar) // İkisinden biri bulunduysa true döndürür
                {
                    return true;
                }
            }

            return false; // Şartlar sağlanmıyorsa false döndğrür
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} alanı en az bir rakam ve bir özel karakter içermelidir.";
        }
    }
}
