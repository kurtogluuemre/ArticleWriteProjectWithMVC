using System.ComponentModel.DataAnnotations;

namespace MVC_CASE.MyValidations
{
    /// <summary>
    /// Validation atarken bazı bilgilerimde ozek karakter olmasin diyorum
    /// </summary>
    public class OzelKarakterOlmasinAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true; // boşluk kontrolü zaten Required attributeu tarafından yapılıyor.

            string stringValue = value.ToString();

            // Eğer özel karakter varsa false döner
            foreach (char c in stringValue)
            {
                if (!char.IsLetterOrDigit(c)) // Harf veya rakam değilse özel karakterdir
                {
                    return false;
                }
            }

            return true;
        }
    }
}
