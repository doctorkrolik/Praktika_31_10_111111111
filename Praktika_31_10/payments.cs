//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Praktika_31_10
{
    using System;
    using System.Collections.Generic;
    
    public partial class payments
    {
        public int PK_payment_id { get; set; }
        public string payment_key { get; set; }
        public decimal price { get; set; }
        public int count { get; set; }
        public decimal sum { get; set; }
        public int FK_product_id { get; set; }
        public int FK_user_id { get; set; }
        public System.DateTime date { get; set; }
        public string purpose { get; set; }
        public string check { get; set; }
    
        public virtual products products { get; set; }
        public virtual users users { get; set; }
    }
}
