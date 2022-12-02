using System.ComponentModel.DataAnnotations;

namespace MVCwithWebAPI.Models
{
    public class Category
    {
        [Key]
        public string category_id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string estimate { get; set; }
        public string importance { get; set; }
        public string due_date { get; set; }
        public string type { get; set; }
    }
}