using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_SocialLogins.Models
{
    public class ProfileModel
    {
        [Required(ErrorMessage = "Have to supply a display name")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Have to supply an e-mail address")]
        [EmailAddress(ErrorMessage ="Pls check email address format")]
        public string Email { get; set; }
    }
}
