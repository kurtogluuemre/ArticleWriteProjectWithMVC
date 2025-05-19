using System.ComponentModel.DataAnnotations;

namespace MVC_CASE.MyValidations
{
    /// <summary>
    /// Validation atarken bazı bilgilerimde rakam olmasin diyorum
    /// </summary>
    public class RakamOlmasinAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true; // boşluk kontrolü zaten Required attributeu tarafından yapılıyor.

            string stringValue = value.ToString();

            foreach (char c in stringValue)
            {
                if (char.IsDigit(c)) // Eğer karakter rakamsa hata
                {
                    return false;
                }
            }

            return true;
        }
    }
}
