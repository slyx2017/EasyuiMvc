//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class g_u_UserInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Sex { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public string Phone { get; set; }
        public string Dept { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Comments { get; set; }
        public string ChangePWDKey { get; set; }
    }
}
