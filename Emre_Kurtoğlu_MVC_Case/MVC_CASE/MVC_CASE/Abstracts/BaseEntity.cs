using MVC_CASE.Enums;

namespace MVC_CASE.Abstracts
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Verinin primaryi keyi 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Verinin kayıt olduğu tarih ilk oluşturulduğu an bir daha değişmeyecek diye datetime now verdim
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Veriyi son güncellediği tarihi tutar
        /// Null olabilir, eğer kayıt hiç güncellenmediyse boş kalır.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Verinin silindiği tarihi tutar
        /// Null olabilir, eğer kayıt silinmemişse boş kalır.
        /// </summary>
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        ///  Mevcut durumunu belirten enum değeri.
        /// </summary>
        public Status Status { get; set; } = Status.Created;
    }
}
