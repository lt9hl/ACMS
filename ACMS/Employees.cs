//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACMS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.DoorsEmployees = new HashSet<DoorsEmployees>();
            this.Keys = new HashSet<Keys>();
        }
    
        public int idEmployee { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Patronymic { get; set; }
        public int idDepartment { get; set; }
        public int idOrganization { get; set; }
        public int idPost { get; set; }
        public string photo { get; set; }
    
        public virtual Departments Departments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoorsEmployees> DoorsEmployees { get; set; }
        public virtual Organizations Organizations { get; set; }
        public virtual Posts Posts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Keys> Keys { get; set; }
    }
}
