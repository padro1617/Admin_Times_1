//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class times_article
    {
        public int id { get; set; }
        public short cat_id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public short click { get; set; }
        public string description { get; set; }
        public byte home_sort { get; set; }
        public bool isshow { get; set; }
        public System.DateTime add_time { get; set; }
        public System.DateTime edit_time { get; set; }
    }
}
