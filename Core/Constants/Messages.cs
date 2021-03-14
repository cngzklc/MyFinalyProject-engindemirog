using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Constants 
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
        public static string UserRegistered ="Kayıt oldu";
        public static string UserNotFound="Kullanıcı bulunamadı";
        public static string PasswordError="Parola Hatası";
        public static string SuccessfulLogin="Başarılı giriş";
        public static string UserAlreadyExists="Kullanıcı mevcut";
        public static string AccessTokenCreated="Erişim isteği oluşturuldu.";
        public static string WrongLoggerType = "Yanlış Kaydedici Türü";
    }
}
