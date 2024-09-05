using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        
        public static string CarAdded = "Araba eklendi";
        public static string CarNameInvalid = "Araba ismi geçersiz";
        public static string CarListed = "Arabalar listelendi";
        public static string RentedCar = "Araba henüz şuebye gelmedi.";

        
        public static string CarImageAdded = "Araba resmi eklendi.";
        public static string CarImageDeleted = "Araba resmi silindi";
        public static string CarImageUpdated = "Araba resmi güncellendi";
        public static string CarImageLimitExceeded = "Bir arabaya en fazla 5 adet resim eklenebilir";
        public static string CarImageNotExist = "Arabaya ait resim yok";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola yanlış";
        public static string SuccesfulyLogin = "Giriş başarılı";
        public static string UserExists = "Kullanıcı  mevcut";
        public static string AccesTokenCreated = "Acces token oluşturuldu.";
        public static string UserRegistered = "Kayıt olundu";
        public static string UserAlreadyExists = "Kullanıcı zaten var";
        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}
