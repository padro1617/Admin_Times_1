//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminLibrary.Model
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// 网站管理员列表
    /// </summary>
    public partial class times_admin
    {
        public byte user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string last_ip { get; set; }
        public System.DateTime add_time { get; set; }
    }
}
