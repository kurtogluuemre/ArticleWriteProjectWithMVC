using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_CASE.MyValidations;
using System.ComponentModel.DataAnnotations;

namespace MVC_CASE.Models.VMs
{
    /// <summary>
    /// Makale oluşturma ve düzenleme işlemleri için kullanılan ViewModel sınıfım.
    /// </summary>
    public class MakaleViewModel
    {
        /// <summary>
        /// Makalenin ID bilgisi (düzenleme işlemleri için kullanılır).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Makale başlığı. Zorunlu alandır.
        /// </summary>
        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        public string Title { get; set; }

        /// <summary>
        /// Makale içeriği. Zorunlu alandır.
        /// </summary>
        [Required(ErrorMessage = "İçerik alanı zorunludur.")]
        public string Content { get; set; }

        /// <summary>
        /// Seçilen kategorilerin ID'leri. En az bir kategori seçilmelidir.
        /// </summary>
        [Required(ErrorMessage = "En az bir kategori seçmelisiniz.")]
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();

        /// <summary>
        /// Kategorilerin listesi. DropDownList veya Checkbox listesi doldurmak için kullanılır.
        /// </summary>
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
