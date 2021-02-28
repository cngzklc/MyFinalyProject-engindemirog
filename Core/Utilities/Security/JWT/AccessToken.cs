using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken // erişim anahtarı: Anlamsız karakterlerde oluşan bir anahar değeridir. Kullanıcıya jeton verirken, jeton şu tarihde bitecek diyoruz.
    {
        public string Token { get; set; } // Jeton
        public DateTime Expiration { get; set; } //Jetonun süresi
    }
}
