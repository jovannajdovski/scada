using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webapi.Enum;
using webapi.Model;

namespace webapi.model
{
    public abstract class InputTagBase:TagBase
    {
        public double ScanTime { get; set; }
        public bool IsScanning { get; set; }
    }
    public abstract class OutputTagBase:TagBase
    {
    }
    public abstract class TagBase
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public IOAddress Address { get; set; }
        public virtual List<TagValue> Values { get; set; }
    }
}
