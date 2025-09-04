using System;
using UtnNoticias;

namespace UtnNoticias.User
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public TipoLenguaje Lenguaje { get; set; }
    }
}
