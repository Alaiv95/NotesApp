using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Models;

public class UserModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
