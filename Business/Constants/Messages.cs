using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi!";
        public static string ProductNameInvalid = "Ürün ismi geçersiz!";
        public static string ProductsListed = "Ürünler listelendi!";
        public static string MaintenanceTime = "Sistem bakımda!";
        public static string ProductCountofCategoryError = "Aynı kategoride ait fazla ürün olmamalı!";
        public static string CategoryLimitExceded = "Bu isimde başka bir ürün var!";
        public static string AuthorizationDenied = "Yetkilendirme Reddedildi";
        internal static string UserRegistered ="Kayıt oldu";
        internal static string UserNotFound="Kullanıcı bulunamadı";
        internal static string PasswordError="Parola Hatası";
        internal static string SuccessfulLogin="Başarılı giriş";
        internal static string UserAlreadyExists="Kullanıcı mevcut";
        internal static string AccessTokenCreated="Erişim isteği oluşturuldu.";
    }
}
