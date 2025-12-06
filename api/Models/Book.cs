using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace api.Models
{
public class Book
{
    public int Id { get; set; }
    [Required]
    [StringLength(17)]
    public string ISBN { get; set; }   
    [Required]
    public string Title { get; set; }
    public string Authors { get; set; } 
    public string Publisher { get; set; }
    public int PublicationYear { get; set; }
    public int TotalCopies { get; set; }      
    public int AvailableCopies { get; set; }  
}

}