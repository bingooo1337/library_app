//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace library
{
    using System;
    using System.Collections.Generic;
    
    public partial class vBlank
    {
        public int ID { get; set; }
        public string status { get; set; }
        public System.DateTime open_date { get; set; }
        public Nullable<System.DateTime> close_date { get; set; }
        public string book_title { get; set; }
        public string author { get; set; }
        public string year_pub { get; set; }
        public int book_id { get; set; }
        public int student_id { get; set; }
    
        public virtual cBook cBook { get; set; }
        public virtual cStudent cStudent { get; set; }
    }
}
